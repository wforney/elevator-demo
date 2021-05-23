// <copyright file="ElevatorDbContext.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Data
{
	using Microsoft.EntityFrameworkCore;

	using System;

	using WebApp.Models;

	/// <summary>
	/// The database context class. Implements the <see cref="DbContext" />.
	/// </summary>
	/// <seealso cref="DbContext" />
	public class ElevatorDbContext : DbContext
	{
		/// <summary>
		/// The created
		/// </summary>
		private static bool Created;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElevatorDbContext" /> class.
		/// </summary>
		/// <param name="dbContextOptions">The database context options.</param>
		public ElevatorDbContext(DbContextOptions<ElevatorDbContext> dbContextOptions)
			: base(dbContextOptions)
		{
			if (Created)
			{
				return;
			}

			Database.Migrate();
			Created = true;
		}

		/// <summary>
		/// Gets the elevators.
		/// </summary>
		/// <value>The elevators.</value>
		public DbSet<Elevator> Elevators => Set<Elevator>();

		/// <summary>
		/// Gets the elevator destinations.
		/// </summary>
		/// <value>The elevator destinations.</value>
		public DbSet<ElevatorDestination> ElevatorDestinations => Set<ElevatorDestination>();

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Elevator>().HasData(new Elevator { CurrentFloor = 1, ElevatorId = 1 });
			modelBuilder.Entity<ElevatorDestination>();

			modelBuilder.Entity<Elevator>().HasMany(e => e.Destinations).WithOne(d => d.Elevator).HasForeignKey(d => d.ElevatorId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
