using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class lab3_eco_Changes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "city_id",
                table: "companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_companies_city_id",
                table: "companies",
                column: "city_id");

            migrationBuilder.AddForeignKey(
                name: "FK_companies_cities_city_id",
                table: "companies",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_cities_city_id",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_city_id",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "companies");
        }
    }
}
