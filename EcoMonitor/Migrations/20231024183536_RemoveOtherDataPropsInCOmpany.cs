using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOtherDataPropsInCOmpany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data1",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "data2",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "data3",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "data4",
                table: "companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "data1",
                table: "companies",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data2",
                table: "companies",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data3",
                table: "companies",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data4",
                table: "companies",
                type: "longtext",
                nullable: true);
        }
    }
}
