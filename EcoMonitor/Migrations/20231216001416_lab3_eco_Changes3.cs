using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class lab3_eco_Changes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_regions_region_id",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_region_id",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "region_id",
                table: "companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_companies_region_id",
                table: "companies",
                column: "region_id");

            migrationBuilder.AddForeignKey(
                name: "FK_companies_regions_region_id",
                table: "companies",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "id");
        }
    }
}
