using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class Add_Damaged_organs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.AddColumn<string>(
                name: "damaged_organs",
                table: "rfc_Factors",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "rfc_factor_id",
                table: "env_Factors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors");

            migrationBuilder.DropColumn(
                name: "damaged_organs",
                table: "rfc_Factors");

            migrationBuilder.AlterColumn<int>(
                name: "rfc_factor_id",
                table: "env_Factors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_env_Factors_rfc_Factors_rfc_factor_id",
                table: "env_Factors",
                column: "rfc_factor_id",
                principalTable: "rfc_Factors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
