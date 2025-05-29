using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseFunctionalityUpdateCompanyNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_Companies_CompanyId",
                table: "CompanyNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_Posts_PostId",
                table: "CompanyNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_RegularUsers_UserId",
                table: "CompanyNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CompanyNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "CompanyNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_Companies_CompanyId",
                table: "CompanyNotifications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_Posts_PostId",
                table: "CompanyNotifications",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_RegularUsers_UserId",
                table: "CompanyNotifications",
                column: "UserId",
                principalTable: "RegularUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_Companies_CompanyId",
                table: "CompanyNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_Posts_PostId",
                table: "CompanyNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_RegularUsers_UserId",
                table: "CompanyNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CompanyNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "CompanyNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_Companies_CompanyId",
                table: "CompanyNotifications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_Posts_PostId",
                table: "CompanyNotifications",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_RegularUsers_UserId",
                table: "CompanyNotifications",
                column: "UserId",
                principalTable: "RegularUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
