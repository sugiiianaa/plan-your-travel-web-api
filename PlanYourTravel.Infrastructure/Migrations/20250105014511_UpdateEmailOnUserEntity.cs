using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTravel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailOnUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Email_Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "Users",
                newName: "Email");
        }
    }
}
