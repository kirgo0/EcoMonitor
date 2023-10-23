using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "passports");

            migrationBuilder.CreateIndex(
                name: "IX_companies_name",
                table: "companies",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_companies_name",
                table: "companies");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "passports",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "");
        }
    }
}
