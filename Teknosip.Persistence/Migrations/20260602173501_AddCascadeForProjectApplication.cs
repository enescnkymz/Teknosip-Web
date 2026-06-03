using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknosip.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeForProjectApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApplications_Projects_ProjectId",
                table: "ProjectApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApplications_Projects_ProjectId",
                table: "ProjectApplications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApplications_Projects_ProjectId",
                table: "ProjectApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApplications_Projects_ProjectId",
                table: "ProjectApplications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
