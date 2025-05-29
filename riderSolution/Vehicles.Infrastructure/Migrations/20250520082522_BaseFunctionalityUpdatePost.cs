using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseFunctionalityUpdatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_VehicleId",
                table: "Posts",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Vehicle_VehicleId",
                table: "Posts",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Vehicle_VehicleId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_VehicleId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Posts");
        }
    }
}
