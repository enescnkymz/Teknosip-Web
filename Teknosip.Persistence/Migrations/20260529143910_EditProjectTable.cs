using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknosip.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingType",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryOrBudget",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "ProjectApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingType",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SalaryOrBudget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "ProjectApplications");
        }
    }
}
