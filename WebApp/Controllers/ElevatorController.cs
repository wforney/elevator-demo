// <copyright file="ElevatorController.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The elevator controller class. Implements the <see cref="ControllerBase" />.
	/// </summary>
	/// <seealso cref="ControllerBase" />
	[ApiController]
	[Route("[controller]")]
	public class ElevatorController : ControllerBase
	{
		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ElevatorController> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElevatorController" /> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		public ElevatorController(ILogger<ElevatorController> logger) => this.logger = logger;

		/// <summary>
		/// Calls for the elevator to be sent to the specified floor number. The elevator may stop
		/// along the way depending on its position.
		/// </summary>
		/// <param name="floor">The floor number.</param>
		/// <remarks>
		/// Requirement: A person requests an elevator be sent to their current floor.
		/// Requirement: A person requests that they be brought to a floor.
		/// </remarks>
		[HttpPost]
		[Route("~/elevator/call/{floor:int}")]
		public async Task CallElevatorTo(int floor)
		{
			using var log = this.logger.BeginScope(nameof(CallElevatorTo));
			await Task.CompletedTask.ConfigureAwait(false);
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
			return await Task.FromResult(1).ConfigureAwait(false);
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
		public async IAsyncEnumerable<int> GetRequestedFloors()
		{
			using var log = this.logger.BeginScope(nameof(GetRequestedFloors));

			await Task.CompletedTask.ConfigureAwait(false);
			yield break;
		}
	}
}
