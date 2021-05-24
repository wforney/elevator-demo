// <copyright file="Elevator.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// The elevator class.
	/// </summary>
	public class Elevator
	{
		/// <summary>
		/// Gets or sets the current floor.
		/// </summary>
		/// <value>The current floor.</value>
		public int CurrentFloor { get; set; } = 1;

		/// <summary>
		/// Gets or sets the destinations.
		/// </summary>
		/// <value>The destinations.</value>
		public ICollection<ElevatorDestination> Destinations { get; set; } = new List<ElevatorDestination>();

		/// <summary>
		/// Gets or sets the elevator identifier.
		/// </summary>
		/// <value>The elevator identifier.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ElevatorId { get; set; }
	}
}
