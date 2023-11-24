using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class RfcNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_rfc_Factors_factor_Name",
                table: "rfc_Factors",
                column: "factor_Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_rfc_Factors_factor_Name",
                table: "rfc_Factors");
        }
    }
}
