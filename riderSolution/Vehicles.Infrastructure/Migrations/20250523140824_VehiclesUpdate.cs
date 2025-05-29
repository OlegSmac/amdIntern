using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VehiclesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Vehicle_Id",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Vehicle_Id",
                table: "Motorcycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Vehicle_VehicleId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Vehicle_Id",
                table: "Trucks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "MaxLoadKg",
                table: "Trucks",
                newName: "TotalWeight");

            migrationBuilder.RenameColumn(
                name: "Passengers",
                table: "Cars",
                newName: "Seats");

            migrationBuilder.AddColumn<string>(
                name: "CabinType",
                table: "Trucks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LoadCapacity",
                table: "Trucks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Cars",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Doors",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicles",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnginePower",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "EngineVolume",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FuelConsumption",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransmissionType",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Vehicles_Id",
                table: "Cars",
                column: "Id",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Vehicles_Id",
                table: "Motorcycles",
                column: "Id",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Vehicles_VehicleId",
                table: "Posts",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Vehicles_Id",
                table: "Trucks",
                column: "Id",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Vehicles_Id",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Vehicles_Id",
                table: "Motorcycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Vehicles_VehicleId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Vehicles_Id",
                table: "Trucks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CabinType",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "LoadCapacity",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Doors",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EnginePower",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EngineVolume",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelConsumption",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TransmissionType",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "TotalWeight",
                table: "Trucks",
                newName: "MaxLoadKg");

            migrationBuilder.RenameColumn(
                name: "Seats",
                table: "Cars",
                newName: "Passengers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Vehicle_Id",
                table: "Cars",
                column: "Id",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Vehicle_Id",
                table: "Motorcycles",
                column: "Id",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Vehicle_VehicleId",
                table: "Posts",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Vehicle_Id",
                table: "Trucks",
                column: "Id",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
