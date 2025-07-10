using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NotificationType",
                table: "Notifications",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Notifications",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                table: "Notifications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AdminId",
                table: "Notifications",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Admins_AdminId",
                table: "Notifications",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Admins_AdminId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AdminId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsResolved",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationType",
                table: "Notifications",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);
        }
    }
}
