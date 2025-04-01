using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPlanMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Exercises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
