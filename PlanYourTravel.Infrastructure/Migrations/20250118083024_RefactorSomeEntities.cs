using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTravel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "Users",
                newName: "EmailAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Users",
                newName: "Email_Value");
        }
    }
}
