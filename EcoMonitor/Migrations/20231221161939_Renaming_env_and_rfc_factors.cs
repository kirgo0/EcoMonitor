using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class Renaming_env_and_rfc_factors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_passports_passport_id",
                table: "env_Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_tax_norms_rfc_Factors_rfc_factor_id",
                table: "tax_norms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rfc_Factors",
                table: "rfc_Factors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_env_Factors",
                table: "env_Factors");

            migrationBuilder.RenameTable(
                name: "rfc_Factors",
                newName: "pollutants");

            migrationBuilder.RenameTable(
                name: "env_Factors",
                newName: "pollutions");

            migrationBuilder.RenameIndex(
                name: "IX_rfc_Factors_factor_Name",
                table: "pollutants",
                newName: "IX_pollutants_factor_Name");

            migrationBuilder.RenameIndex(
                name: "IX_env_Factors_rfc_factor_id",
                table: "pollutions",
                newName: "IX_pollutions_rfc_factor_id");

            migrationBuilder.RenameIndex(
                name: "IX_env_Factors_passport_id",
                table: "pollutions",
                newName: "IX_pollutions_passport_id");

            migrationBuilder.RenameIndex(
                name: "IX_env_Factors_factor_Name_passport_id",
                table: "pollutions",
                newName: "IX_pollutions_factor_Name_passport_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pollutants",
                table: "pollutants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pollutions",
                table: "pollutions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_pollutions_passports_passport_id",
                table: "pollutions",
                column: "passport_id",
                principalTable: "passports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pollutions_pollutants_rfc_factor_id",
                table: "pollutions",
                column: "rfc_factor_id",
                principalTable: "pollutants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tax_norms_pollutants_rfc_factor_id",
                table: "tax_norms",
                column: "rfc_factor_id",
                principalTable: "pollutants",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pollutions_passports_passport_id",
                table: "pollutions");

            migrationBuilder.DropForeignKey(
                name: "FK_pollutions_pollutants_rfc_factor_id",
                table: "pollutions");

            migrationBuilder.DropForeignKey(
                name: "FK_tax_norms_pollutants_rfc_factor_id",
                table: "tax_norms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pollutions",
                table: "pollutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pollutants",
                table: "pollutants");

            migrationBuilder.RenameTable(
                name: "pollutions",
                newName: "env_Factors");

            migrationBuilder.RenameTable(
                name: "pollutants",
                newName: "rfc_Factors");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_rfc_factor_id",
                table: "env_Factors",
                newName: "IX_env_Factors_rfc_factor_id");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_passport_id",
                table: "env_Factors",
                newName: "IX_env_Factors_passport_id");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_factor_Name_passport_id",
                table: "env_Factors",
                newName: "IX_env_Factors_factor_Name_passport_id");

            migrationBuilder.RenameIndex(
                name: "IX_pollutants_factor_Name",
                table: "rfc_Factors",
                newName: "IX_rfc_Factors_factor_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_env_Factors",
                table: "env_Factors",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rfc_Factors",
                table: "rfc_Factors",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_passports_passport_id",
                table: "env_Factors",
                column: "passport_id",
                principalTable: "passports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tax_norms_rfc_Factors_rfc_factor_id",
                table: "tax_norms",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id");
        }
    }
}
