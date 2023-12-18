using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class lab3_eco_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GDK_value",
                table: "rfc_Factors",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SF_value",
                table: "rfc_Factors",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "mass_flow_rate",
                table: "rfc_Factors",
                type: "double",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    population = table.Column<int>(type: "int", nullable: false),
                    region_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                    table.ForeignKey(
                        name: "FK_cities_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cities_name",
                table: "cities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cities_region_id",
                table: "cities",
                column: "region_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropColumn(
                name: "GDK_value",
                table: "rfc_Factors");

            migrationBuilder.DropColumn(
                name: "SF_value",
                table: "rfc_Factors");

            migrationBuilder.DropColumn(
                name: "mass_flow_rate",
                table: "rfc_Factors");
        }
    }
}
