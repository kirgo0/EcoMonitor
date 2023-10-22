using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_env_Factors_passport_id",
                table: "env_Factors",
                column: "passport_id");

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_passports_passport_id",
                table: "env_Factors",
                column: "passport_id",
                principalTable: "passports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_passports_passport_id",
                table: "env_Factors");

            migrationBuilder.DropIndex(
                name: "IX_env_Factors_passport_id",
                table: "env_Factors");
        }
    }
}
