using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class fix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "companies");

            migrationBuilder.AddColumn<string>(
                name: "data",
                table: "passports",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "passports");

            migrationBuilder.AddColumn<string>(
                name: "data",
                table: "companies",
                type: "longtext",
                nullable: true);
        }
    }
}
