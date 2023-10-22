using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_passports_company_id",
                table: "passports",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_passports_companies_company_id",
                table: "passports",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_passports_companies_company_id",
                table: "passports");

            migrationBuilder.DropIndex(
                name: "IX_passports_company_id",
                table: "passports");
        }
    }
}
