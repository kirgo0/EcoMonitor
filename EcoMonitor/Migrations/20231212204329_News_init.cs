using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class News_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "companies");

            migrationBuilder.AddColumn<int>(
                name: "region_id",
                table: "companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    post_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    population = table.Column<int>(type: "int", nullable: false),
                    square = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CompanyNews",
                columns: table => new
                {
                    companyid = table.Column<int>(type: "int", nullable: false),
                    newsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNews", x => new { x.companyid, x.newsid });
                    table.ForeignKey(
                        name: "FK_CompanyNews_companies_companyid",
                        column: x => x.companyid,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyNews_news_newsid",
                        column: x => x.newsid,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NewsUser",
                columns: table => new
                {
                    authorId = table.Column<string>(type: "varchar(255)", nullable: false),
                    newsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsUser", x => new { x.authorId, x.newsid });
                    table.ForeignKey(
                        name: "FK_NewsUser_AspNetUsers_authorId",
                        column: x => x.authorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsUser_news_newsid",
                        column: x => x.newsid,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NewsRegion",
                columns: table => new
                {
                    newsid = table.Column<int>(type: "int", nullable: false),
                    regionsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsRegion", x => new { x.newsid, x.regionsid });
                    table.ForeignKey(
                        name: "FK_NewsRegion_news_newsid",
                        column: x => x.newsid,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsRegion_regions_regionsid",
                        column: x => x.regionsid,
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_companies_region_id",
                table: "companies",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNews_newsid",
                table: "CompanyNews",
                column: "newsid");

            migrationBuilder.CreateIndex(
                name: "IX_news_title",
                table: "news",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsRegion_regionsid",
                table: "NewsRegion",
                column: "regionsid");

            migrationBuilder.CreateIndex(
                name: "IX_NewsUser_newsid",
                table: "NewsUser",
                column: "newsid");

            migrationBuilder.CreateIndex(
                name: "IX_regions_name",
                table: "regions",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_companies_regions_region_id",
                table: "companies",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_regions_region_id",
                table: "companies");

            migrationBuilder.DropTable(
                name: "CompanyNews");

            migrationBuilder.DropTable(
                name: "NewsRegion");

            migrationBuilder.DropTable(
                name: "NewsUser");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropIndex(
                name: "IX_companies_region_id",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "companies");

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "companies",
                type: "longtext",
                nullable: true);
        }
    }
}
