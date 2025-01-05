using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTravel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateForeignKeyForFlightTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightTransactions_FlightSchedules_FlightScheduleId",
                table: "FlightTransactions");

            migrationBuilder.DropIndex(
                name: "IX_FlightTransactions_FlightScheduleId",
                table: "FlightTransactions");

            migrationBuilder.DropColumn(
                name: "FlightScheduleId",
                table: "FlightTransactions");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "SeatClassPriceAtBooking",
                table: "FlightTransactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "PaidAmount",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "SeatClassPriceAtBooking",
                table: "FlightTransactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<Guid>(
                name: "FlightScheduleId",
                table: "FlightTransactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FlightTransactions_FlightScheduleId",
                table: "FlightTransactions",
                column: "FlightScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightTransactions_FlightSchedules_FlightScheduleId",
                table: "FlightTransactions",
                column: "FlightScheduleId",
                principalTable: "FlightSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
