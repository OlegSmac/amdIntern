using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelYears_Models_ModelId",
                table: "ModelYears");

            migrationBuilder.DropIndex(
                name: "IX_ModelYears_ModelId",
                table: "ModelYears");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "ModelYears");

            migrationBuilder.CreateTable(
                name: "ModelYear",
                columns: table => new
                {
                    ModelsId = table.Column<int>(type: "int", nullable: false),
                    YearsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelYear", x => new { x.ModelsId, x.YearsId });
                    table.ForeignKey(
                        name: "FK_ModelYear_ModelYears_YearsId",
                        column: x => x.YearsId,
                        principalTable: "ModelYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelYear_Models_ModelsId",
                        column: x => x.ModelsId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelYear_YearsId",
                table: "ModelYear",
                column: "YearsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelYear");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "ModelYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ModelYears_ModelId",
                table: "ModelYears",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelYears_Models_ModelId",
                table: "ModelYears",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
