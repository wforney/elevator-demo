// <copyright file="ElevatorService.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Services
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using WebApp.Data;

	/// <summary>
	/// The elevator service class.
	/// </summary>
	/// <remarks>
	/// This is a wrapper around some third party library that actually moves the elevator where we
	/// tell it to.
	/// </remarks>
	public class ElevatorService : IElevatorService
	{
		/// <summary>
		/// The elevator database context
		/// </summary>
		private readonly ElevatorDbContext elevatorDbContext;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ElevatorService> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElevatorService" /> class.
		/// </summary>
		/// <param name="elevatorDbContext">The elevator database context.</param>
		/// <param name="logger">The logger.</param>
		public ElevatorService(ElevatorDbContext elevatorDbContext, ILogger<ElevatorService> logger)
		{
			this.elevatorDbContext = elevatorDbContext ?? throw new ArgumentNullException(nameof(elevatorDbContext));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public async Task MoveToFloor(int elevatorId, int floor)
		{
			// TODO: Make the elevator actually move.

			// Clean up the destination entries for this floor since we are there now that we moved.
			var destinationsToRemove = await this.elevatorDbContext.ElevatorDestinations
				.Where(d => d.ElevatorId == elevatorId)
				.Where(d => d.FloorNumber == floor)
				.ToArrayAsync()
				.ConfigureAwait(false);

			this.elevatorDbContext.ElevatorDestinations.RemoveRange(destinationsToRemove);

			await this.elevatorDbContext.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}
