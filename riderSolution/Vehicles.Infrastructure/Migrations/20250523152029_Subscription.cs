using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Subscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNotifications_RegularUsers_UserId",
                table: "CompanyNotifications");

            migrationBuilder.DropIndex(
                name: "IX_CompanyNotifications_UserId",
                table: "CompanyNotifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CompanyNotifications");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => new { x.UserId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Subscriptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_RegularUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RegularUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserNotifications_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserNotifications_RegularUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RegularUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CompanyId",
                table: "Subscriptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_CompanyId",
                table: "UserNotifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_PostId",
                table: "UserNotifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UserId",
                table: "UserNotifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CompanyNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNotifications_UserId",
                table: "CompanyNotifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNotifications_RegularUsers_UserId",
                table: "CompanyNotifications",
                column: "UserId",
                principalTable: "RegularUsers",
                principalColumn: "Id");
        }
    }
}
