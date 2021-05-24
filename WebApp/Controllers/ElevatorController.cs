// <copyright file="ElevatorController.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using WebApp.Data;
	using WebApp.Models;
	using WebApp.Properties;
	using WebApp.Services;

	/// <summary>
	/// The elevator controller class. Implements the <see cref="ControllerBase" />.
	/// </summary>
	/// <seealso cref="ControllerBase" />
	[ApiController]
	[Route("[controller]")]
	public class ElevatorController : ControllerBase
	{
		/// <summary>
		/// The elevator identifier for this demo. In a real system I would assume there is more
		/// than one elevator being controlled by this and would pass this in instead.
		/// </summary>
		private const int ElevatorId = 1;

		/// <summary>
		/// The elevator database context
		/// </summary>
		private readonly ElevatorDbContext elevatorDbContext;

		/// <summary>
		/// The elevator service
		/// </summary>
		private readonly IElevatorService elevatorService;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ElevatorController> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElevatorController" /> class.
		/// </summary>
		/// <param name="elevatorDbContext">The elevator database context.</param>
		/// <param name="elevatorService">The elevator service.</param>
		/// <param name="logger">The logger.</param>
		public ElevatorController(ElevatorDbContext elevatorDbContext, IElevatorService elevatorService, ILogger<ElevatorController> logger)
		{
			this.elevatorDbContext = elevatorDbContext;
			this.elevatorService = elevatorService;
			this.logger = logger;
		}

		/// <summary>
		/// Calls for the elevator to be sent to the specified floor number. The elevator may stop
		/// along the way depending on its position.
		/// </summary>
		/// <param name="floor">The floor number.</param>
		/// <remarks>
		/// <para>
		/// Requirement: A person requests an elevator be sent to their current floor. <br />
		/// Requirement: A person requests that they be brought to a floor.
		/// </para>
		/// <para>
		/// NOTE: The negative floor number check is just an example. I realize basement levels
		/// could be negative depending on how you number things.
		/// </para>
		/// </remarks>
		/// <exception cref="ArgumentOutOfRangeException">
		/// The floor number cannot be less than one.
		/// </exception>
		[HttpPost]
		[Route("~/elevator/call/{floor:int}")]
		public async Task CallElevatorTo(int floor)
		{
			using var log = this.logger.BeginScope(nameof(CallElevatorTo));

			if (floor < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(floor), Resources.FloorCannotBeLessThanOne);
			}

			// If the floor has already been requested log the additional request to trace and ignore.
			if (await this.elevatorDbContext.ElevatorDestinations.AnyAsync(ed => ed.FloorNumber == floor).ConfigureAwait(false))
			{
				this.logger.LogTrace("Floor {floor} already requested.", floor);
			}
			else
			{
				// Get the next destination floor and check the current floor number of the elevator.
				var nextFloor = await this.GetNextFloor().ConfigureAwait(false);
				var currentFloor = (await this.elevatorDbContext.Elevators.FindAsync(ElevatorId).ConfigureAwait(false)).CurrentFloor;

				// If the requested floor matches the next destination floor log the request to
				// trace and ignore.
				if (floor == nextFloor)
				{
					this.logger.LogTrace("The elevator is already going to floor {floor}.", floor);
					return;
				}

				// If the current floor number matches the requested floor number log the request to
				// trace and ignore; otherwise, save the request to the database and call the
				// elevator routine to get the car moving.
				if (nextFloor == currentFloor)
				{
					this.logger.LogTrace("The elevator is already on floor {floor}.", floor);
					return;
				}

				// Save the request for the specified floor to the database.
				this.elevatorDbContext.ElevatorDestinations.Add(new ElevatorDestination { ElevatorId = ElevatorId, FloorNumber = floor });
				await this.elevatorDbContext.SaveChangesAsync().ConfigureAwait(false);

				this.logger.LogInformation("Floor {floor} requested.", floor);

				// Calls the third party wrapper to actually do the moving.
				await this.elevatorService.MoveToFloor(ElevatorId, floor).ConfigureAwait(false);
			}
		}

		/// <summary>
		/// Gets the number of the next floor in the list of requested floors.
		/// </summary>
		/// <returns>The number of the next floor in the list of requested floors.</returns>
		/// <remarks>Requirement: An elevator car requests the next floor it needs to service.</remarks>
		[HttpGet]
		[Route("~/elevator/next")]
		public async Task<int> GetNextFloor()
		{
			using var log = this.logger.BeginScope(nameof(GetNextFloor));

			var currentFloor = (await this.elevatorDbContext.Elevators.FindAsync(ElevatorId).ConfigureAwait(false)).CurrentFloor;

			var destinations = this.elevatorDbContext.ElevatorDestinations
				.Where(e => e.ElevatorId == ElevatorId)
				.OrderBy(e => e.FloorNumber);

			async Task RemoveRedundantDestinations(int floor)
			{
				using var log = this.logger.BeginScope(nameof(RemoveRedundantDestinations));

				// NOTE: I really don't like how EF handles deletes like this. There is a way to
				// make it not query first, but I'll leave that for later.
				var redundantDestinations = await this.elevatorDbContext.ElevatorDestinations
					.Where(d => d.ElevatorId == ElevatorId)
					.Where(d => d.FloorNumber == floor)
					.ToArrayAsync()
					.ConfigureAwait(false);

				this.elevatorDbContext.ElevatorDestinations.RemoveRange(redundantDestinations);
				await this.elevatorDbContext.SaveChangesAsync().ConfigureAwait(false);
			}

			var countOfDestinations = await destinations.CountAsync().ConfigureAwait(false);
			if (countOfDestinations == 0 || await destinations.AllAsync(d => d.FloorNumber == currentFloor).ConfigureAwait(false))
			{
				// clean up and stay put
				await RemoveRedundantDestinations(currentFloor).ConfigureAwait(false);
				return currentFloor;
			}

			var destinationsAboveCurrentFloor = destinations.Where(d => d.FloorNumber > currentFloor);
			var destinationsAboveCurrentFloorCount = await destinationsAboveCurrentFloor.CountAsync().ConfigureAwait(false);

			var destinationsBelowCurrentFloor = destinations.Where(d => d.FloorNumber < currentFloor);
			var destinationsBelowCurrentFloorCount = await destinationsBelowCurrentFloor.CountAsync().ConfigureAwait(false);

			if (destinationsAboveCurrentFloorCount > 0 && destinationsAboveCurrentFloorCount > destinationsBelowCurrentFloorCount)
			{
				// go up
				return (await destinationsAboveCurrentFloor.FirstAsync().ConfigureAwait(false)).FloorNumber;
			}

			if (destinationsBelowCurrentFloorCount == 0)
			{
				if (destinationsAboveCurrentFloorCount > 0)
				{
					// go up
					return (await destinationsAboveCurrentFloor.FirstAsync().ConfigureAwait(false)).FloorNumber;
				}

				// clean up and stay put
				await RemoveRedundantDestinations(currentFloor).ConfigureAwait(false);
				return currentFloor;
			}

			// go down
			return (await destinationsBelowCurrentFloor.LastAsync().ConfigureAwait(false)).FloorNumber;
		}

		/// <summary>
		/// Gets the numbers of the requested floors.
		/// </summary>
		/// <returns>The numbers of the requested floors.</returns>
		/// <remarks>
		/// Requirement: An elevator car requests all floors that it’s current passengers are
		/// servicing (e.g. to light up the buttons that show which floors the car is going to).
		/// </remarks>
		[HttpGet]
		[Route("~/elevator/queue")]
		public IAsyncEnumerable<int> GetRequestedFloors()
		{
			using var log = this.logger.BeginScope(nameof(GetRequestedFloors));

			return this.elevatorDbContext.ElevatorDestinations
				.Where(e => e.ElevatorId == ElevatorId)
				.Select(ed => ed.FloorNumber)
				.Distinct()
				.OrderBy(floorNumber => floorNumber)
				.AsAsyncEnumerable();
		}
	}
}
