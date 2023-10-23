using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class uniqueConstraintEnvFactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_env_Factors_factor_Name_passport_id",
                table: "env_Factors",
                columns: new[] { "factor_Name", "passport_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_env_Factors_factor_Name_passport_id",
                table: "env_Factors");
        }
    }
}
