// <copyright file="ElevatorQueue.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// The elevator destination class.
	/// </summary>
	public class ElevatorDestination
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ElevatorDestinationId { get; set; }

		/// <summary>
		/// Gets or sets the elevator.
		/// </summary>
		/// <value>The elevator.</value>
		public Elevator Elevator { get; set; } = new Elevator();

		/// <summary>
		/// Gets or sets the elevator identifier.
		/// </summary>
		/// <value>The elevator identifier.</value>
		public int ElevatorId { get; set; }

		/// <summary>
		/// Gets or sets the floor number.
		/// </summary>
		/// <value>The floor number.</value>
		public int FloorNumber { get; set; }
	}
}
