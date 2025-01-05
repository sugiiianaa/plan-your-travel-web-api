using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTravel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyRelationshipOfFlightTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FlightTransactions_FlightScheduleId",
                table: "FlightTransactions",
                column: "FlightScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightTransactions_FlightSeatClassId",
                table: "FlightTransactions",
                column: "FlightSeatClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightTransactions_FlightSchedules_FlightScheduleId",
                table: "FlightTransactions",
                column: "FlightScheduleId",
                principalTable: "FlightSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightTransactions_FlightSeatClasses_FlightSeatClassId",
                table: "FlightTransactions",
                column: "FlightSeatClassId",
                principalTable: "FlightSeatClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightTransactions_FlightSchedules_FlightScheduleId",
                table: "FlightTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightTransactions_FlightSeatClasses_FlightSeatClassId",
                table: "FlightTransactions");

            migrationBuilder.DropIndex(
                name: "IX_FlightTransactions_FlightScheduleId",
                table: "FlightTransactions");

            migrationBuilder.DropIndex(
                name: "IX_FlightTransactions_FlightSeatClassId",
                table: "FlightTransactions");
        }
    }
}
