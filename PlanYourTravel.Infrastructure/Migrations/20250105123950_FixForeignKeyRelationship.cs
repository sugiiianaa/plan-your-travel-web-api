using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTravel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FlightSchedules_AirlineId",
                table: "FlightSchedules",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchedules_ArrivalAirportId",
                table: "FlightSchedules",
                column: "ArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchedules_DepartureAirportId",
                table: "FlightSchedules",
                column: "DepartureAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSchedules_Airlines_AirlineId",
                table: "FlightSchedules",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSchedules_Airports_ArrivalAirportId",
                table: "FlightSchedules",
                column: "ArrivalAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSchedules_Airports_DepartureAirportId",
                table: "FlightSchedules",
                column: "DepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightSchedules_Airlines_AirlineId",
                table: "FlightSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSchedules_Airports_ArrivalAirportId",
                table: "FlightSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSchedules_Airports_DepartureAirportId",
                table: "FlightSchedules");

            migrationBuilder.DropIndex(
                name: "IX_FlightSchedules_AirlineId",
                table: "FlightSchedules");

            migrationBuilder.DropIndex(
                name: "IX_FlightSchedules_ArrivalAirportId",
                table: "FlightSchedules");

            migrationBuilder.DropIndex(
                name: "IX_FlightSchedules_DepartureAirportId",
                table: "FlightSchedules");
        }
    }
}
