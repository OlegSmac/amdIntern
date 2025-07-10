using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePostIdFromNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Posts_PostId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Posts_UserNotification_PostId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PostId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserNotification_PostId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserNotification_PostId",
                table: "Notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserNotification_PostId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostId",
                table: "Notifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserNotification_PostId",
                table: "Notifications",
                column: "UserNotification_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Posts_PostId",
                table: "Notifications",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Posts_UserNotification_PostId",
                table: "Notifications",
                column: "UserNotification_PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
