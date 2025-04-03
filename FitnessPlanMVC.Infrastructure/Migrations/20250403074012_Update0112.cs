using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPlanMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update0112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenges_AspNetUsers_ApplicationUserId",
                table: "UserChallenges");

            migrationBuilder.DropIndex(
                name: "IX_UserChallenges_ApplicationUserId",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Challenges");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserChallenges",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_UserId",
                table: "UserChallenges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenges_AspNetUsers_UserId",
                table: "UserChallenges",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenges_AspNetUsers_UserId",
                table: "UserChallenges");

            migrationBuilder.DropIndex(
                name: "IX_UserChallenges_UserId",
                table: "UserChallenges");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserChallenges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserChallenges",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "UserChallenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Goal",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_ApplicationUserId",
                table: "UserChallenges",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenges_AspNetUsers_ApplicationUserId",
                table: "UserChallenges",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
