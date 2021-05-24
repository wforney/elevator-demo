// <copyright file="IElevatorService.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Services
{
	using System.Threading.Tasks;

	/// <summary>
	/// The elevator service interface.
	/// </summary>
	/// <remarks>
	/// This is a wrapper around some third party library that actually moves the elevator where we tell it to.
	/// </remarks>
	public interface IElevatorService
	{
		/// <summary>
		/// Moves the elevator to the specified floor.
		/// </summary>
		/// <param name="elevatorId">The elevator identifier.</param>
		/// <param name="floor">The floor.</param>
		Task MoveToFloor(int elevatorId, int floor);
	}
}
