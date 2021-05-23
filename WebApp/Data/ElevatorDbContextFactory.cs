// <copyright file="AtmDbContextFactory.cs" company="William Forney">
//     Copyright © 2021 William Forney. All Rights Reserved.
// </copyright>

namespace WebApp.Data
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;

	/// <summary>
	/// The database context factory class. Implements the <see
	/// cref="IDesignTimeDbContextFactory{ElevatorDbContext}" />
	/// </summary>
	/// <seealso cref="IDesignTimeDbContextFactory{ElevatorDbContext}" />
	public class ElevatorDbContextFactory : IDesignTimeDbContextFactory<ElevatorDbContext>
	{
		/// <summary>
		/// Creates a new instance of a derived context.
		/// </summary>
		/// <param name="args">Arguments provided by the design-time service.</param>
		/// <returns>An instance of ElevatorDbContext.</returns>
		public ElevatorDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ElevatorDbContext>();
			optionsBuilder.UseSqlite("Data Source=Database.db");

			return new ElevatorDbContext(optionsBuilder.Options);
		}
	}
}
