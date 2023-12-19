using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class Taxes_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tax_norm_id",
                table: "rfc_Factors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "source_operating_time",
                table: "passports",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "isResort",
                table: "cities",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tax_norms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    factor_Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    air_emissions = table.Column<double>(type: "double", nullable: false),
                    water_emissions = table.Column<double>(type: "double", nullable: false),
                    disposal_of_wastes = table.Column<double>(type: "double", nullable: false),
                    radioactive_wastes = table.Column<double>(type: "double", nullable: false),
                    temporary_disposal_of_radioactive_wastes = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_norms", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rfc_Factors_tax_norms_tax_norm_id",
                table: "rfc_Factors");

            migrationBuilder.DropTable(
                name: "tax_norms");

            migrationBuilder.DropIndex(
                name: "IX_rfc_Factors_tax_norm_id",
                table: "rfc_Factors");

            migrationBuilder.DropColumn(
                name: "tax_norm_id",
                table: "rfc_Factors");

            migrationBuilder.DropColumn(
                name: "source_operating_time",
                table: "passports");

            migrationBuilder.DropColumn(
                name: "isResort",
                table: "cities");
        }
    }
}
