using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.AlterColumn<int>(
                name: "rfc_factor_id",
                table: "env_Factors",
                nullable: true); 

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.AlterColumn<int>(
                name: "rfc_factor_id",
                table: "env_Factors",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
