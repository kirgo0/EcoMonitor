using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class EnvFactorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "factor_Ca_value",
                table: "env_Factors",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "factor_Ch_value",
                table: "env_Factors",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "factor_Ca_value",
                table: "env_Factors");

            migrationBuilder.DropColumn(
                name: "factor_Ch_value",
                table: "env_Factors");
        }
    }
}
