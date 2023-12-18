using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class news_likes_updat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data2",
                table: "passports");

            migrationBuilder.DropColumn(
                name: "data3",
                table: "passports");

            migrationBuilder.DropColumn(
                name: "data4",
                table: "passports");

            migrationBuilder.DropColumn(
                name: "data5",
                table: "passports");

            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "news",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NewsFollowers",
                columns: table => new
                {
                    followersId = table.Column<string>(type: "varchar(255)", nullable: false),
                    likedNewsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFollowers", x => new { x.followersId, x.likedNewsid });
                    table.ForeignKey(
                        name: "FK_NewsFollowers_AspNetUsers_followersId",
                        column: x => x.followersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsFollowers_news_likedNewsid",
                        column: x => x.likedNewsid,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFollowers_likedNewsid",
                table: "NewsFollowers",
                column: "likedNewsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsFollowers");

            migrationBuilder.DropColumn(
                name: "likes",
                table: "news");

            migrationBuilder.AddColumn<string>(
                name: "data2",
                table: "passports",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data3",
                table: "passports",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data4",
                table: "passports",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data5",
                table: "passports",
                type: "longtext",
                nullable: true);
        }
    }
}
