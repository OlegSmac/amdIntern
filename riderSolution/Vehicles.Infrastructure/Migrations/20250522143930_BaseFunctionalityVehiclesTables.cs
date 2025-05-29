using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseFunctionalityVehiclesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "HasSidecar",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "MaxLoadKg",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Passengers",
                table: "Vehicle");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Passengers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Vehicle_Id",
                        column: x => x.Id,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    HasSidecar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorcycles_Vehicle_Id",
                        column: x => x.Id,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaxLoadKg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_Vehicle_Id",
                        column: x => x.Id,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Vehicle",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasSidecar",
                table: "Vehicle",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxLoadKg",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Passengers",
                table: "Vehicle",
                type: "int",
                nullable: true);
        }
    }
}
