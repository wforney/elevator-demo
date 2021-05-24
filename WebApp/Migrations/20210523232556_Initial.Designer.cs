// <copyright file="20210523232556_Initial.Designer.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

namespace WebApp.Migrations
{
	/// <summary>
	/// The Initial class.
	/// Implements the <see cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
	/// </summary>
	/// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
	[DbContext(typeof(ElevatorDbContext))]
    [Migration("20210523232556_Initial")]
    partial class Initial
    {
		/// <summary>
		/// Implemented to build the <see cref="P:Microsoft.EntityFrameworkCore.Migrations.Migration.TargetModel" />.
		/// </summary>
		/// <param name="modelBuilder">The <see cref="T:Microsoft.EntityFrameworkCore.ModelBuilder" /> to use to build the model.</param>
		protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("WebApp.Models.Elevator", b =>
                {
                    b.Property<int>("ElevatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentFloor")
                        .HasColumnType("INTEGER");

                    b.HasKey("ElevatorId");

                    b.ToTable("Elevators");

                    b.HasData(
                        new
                        {
                            ElevatorId = 1,
                            CurrentFloor = 1
                        });
                });

            modelBuilder.Entity("WebApp.Models.ElevatorDestination", b =>
                {
                    b.Property<int>("ElevatorDestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ElevatorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("ElevatorDestinationId");

                    b.HasIndex("ElevatorId");

                    b.ToTable("ElevatorDestinations");
                });

            modelBuilder.Entity("WebApp.Models.ElevatorDestination", b =>
                {
                    b.HasOne("WebApp.Models.Elevator", "Elevator")
                        .WithMany("Destinations")
                        .HasForeignKey("ElevatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Elevator");
                });

            modelBuilder.Entity("WebApp.Models.Elevator", b =>
                {
                    b.Navigation("Destinations");
                });
#pragma warning restore 612, 618
        }
    }
}
