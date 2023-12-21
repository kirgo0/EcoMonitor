using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class little_year_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "year",
                table: "tax_norms",
                type: "YEAR(4)",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "YEAR(4)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "year",
                table: "tax_norms",
                type: "YEAR(4)",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "YEAR(4)");
        }
    }
}
