using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class RfcKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rfc_factor_id",
                table: "env_Factors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_env_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.DropIndex(
                name: "IX_env_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.DropColumn(
                name: "rfc_factor_id",
                table: "env_Factors");
        }
    }
}
