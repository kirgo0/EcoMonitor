using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class Managed_Connection_Between_TaxAndRfc_As_Many_To_One : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rfc_Factors_tax_norms_tax_norm_id",
                table: "rfc_Factors");

            migrationBuilder.DropIndex(
                name: "IX_rfc_Factors_tax_norm_id",
                table: "rfc_Factors");

            migrationBuilder.DropColumn(
                name: "tax_norm_id",
                table: "rfc_Factors");

            migrationBuilder.AddColumn<int>(
                name: "rfc_factor_id",
                table: "tax_norms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "tax_norms",
                type: "YEAR(4)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tax_norms_rfc_factor_id",
                table: "tax_norms",
                column: "rfc_factor_id");

            migrationBuilder.CreateIndex(
                name: "IX_tax_norms_year_factor_Name",
                table: "tax_norms",
                columns: new[] { "year", "factor_Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tax_norms_rfc_Factors_rfc_factor_id",
                table: "tax_norms",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tax_norms_rfc_Factors_rfc_factor_id",
                table: "tax_norms");

            migrationBuilder.DropIndex(
                name: "IX_tax_norms_rfc_factor_id",
                table: "tax_norms");

            migrationBuilder.DropIndex(
                name: "IX_tax_norms_year_factor_Name",
                table: "tax_norms");

            migrationBuilder.DropColumn(
                name: "rfc_factor_id",
                table: "tax_norms");

            migrationBuilder.DropColumn(
                name: "year",
                table: "tax_norms");

            migrationBuilder.AddColumn<int>(
                name: "tax_norm_id",
                table: "rfc_Factors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_rfc_Factors_tax_norm_id",
                table: "rfc_Factors",
                column: "tax_norm_id");

            migrationBuilder.AddForeignKey(
                name: "FK_rfc_Factors_tax_norms_tax_norm_id",
                table: "rfc_Factors",
                column: "tax_norm_id",
                principalTable: "tax_norms",
                principalColumn: "id");
        }
    }
}
