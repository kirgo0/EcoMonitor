using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class CompanyWaste_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "radioactive_disposal_time",
                table: "env_Factors",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "radioactive_volume",
                table: "env_Factors",
                type: "double",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "company_wastes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    passport_id = table.Column<int>(type: "int", nullable: false),
                    Koc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ko = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Kt = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_wastes", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_wastes_passports_passport_id",
                        column: x => x.passport_id,
                        principalTable: "passports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_company_wastes_passport_id",
                table: "company_wastes",
                column: "passport_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_wastes");

            migrationBuilder.DropColumn(
                name: "radioactive_disposal_time",
                table: "env_Factors");

            migrationBuilder.DropColumn(
                name: "radioactive_volume",
                table: "env_Factors");
        }
    }
}
