// <copyright file="20210523232556_Initial.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Migrations
{
	using Microsoft.EntityFrameworkCore.Migrations;

	/// <summary>
	/// The Initial class. Implements the <see cref="Migration" />
	/// </summary>
	/// <seealso cref="Migration" />
	public partial class Initial : Migration
	{
		/// <summary>
		/// <para>Builds the operations that will migrate the database 'down'.</para>
		/// <para>
		/// That is, builds the operations that will take the database from the state left in by
		/// this migration so that it returns to the state that it was in before this migration was applied.
		/// </para>
		/// <para>
		/// This method must be overridden in each class the inherits from <see
		/// cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" /> if both 'up' and 'down'
		/// migrations are to be supported. If it is not overridden, then calling it will throw and
		/// it will not be possible to migrate in the 'down' direction.
		/// </para>
		/// </summary>
		/// <param name="migrationBuilder">
		/// The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will
		/// build the operations.
		/// </param>
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ElevatorDestinations");

			migrationBuilder.DropTable(
				name: "Elevators");
		}

		/// <summary>
		/// <para>Builds the operations that will migrate the database 'up'.</para>
		/// <para>
		/// That is, builds the operations that will take the database from the state left in by the
		/// previous migration so that it is up-to-date with regard to this migration.
		/// </para>
		/// <para>
		/// This method must be overridden in each class the inherits from <see
		/// cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" />.
		/// </para>
		/// </summary>
		/// <param name="migrationBuilder">
		/// The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will
		/// build the operations.
		/// </param>
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Elevators",
				columns: table => new
				{
					ElevatorId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					CurrentFloor = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table => table.PrimaryKey("PK_Elevators", x => x.ElevatorId));

			migrationBuilder.CreateTable(
				name: "ElevatorDestinations",
				columns: table => new
				{
					ElevatorDestinationId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					ElevatorId = table.Column<int>(type: "INTEGER", nullable: false),
					FloorNumber = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ElevatorDestinations", x => x.ElevatorDestinationId);
					table.ForeignKey(
						name: "FK_ElevatorDestinations_Elevators_ElevatorId",
						column: x => x.ElevatorId,
						principalTable: "Elevators",
						principalColumn: "ElevatorId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "Elevators",
				columns: new[] { "ElevatorId", "CurrentFloor" },
				values: new object[] { 1, 1 });

			migrationBuilder.CreateIndex(
				name: "IX_ElevatorDestinations_ElevatorId",
				table: "ElevatorDestinations",
				column: "ElevatorId");
		}
	}
}
