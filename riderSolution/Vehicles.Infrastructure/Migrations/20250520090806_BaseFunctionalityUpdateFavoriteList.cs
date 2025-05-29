using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseFunctionalityUpdateFavoriteList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLists_Companies_CompanyId",
                table: "FavoriteLists");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteLists_CompanyId",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FavoriteLists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "FavoriteLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FavoriteLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLists_CompanyId",
                table: "FavoriteLists",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLists_Companies_CompanyId",
                table: "FavoriteLists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
