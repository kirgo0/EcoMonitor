using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class global_renaming_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNews_companies_companyid",
                table: "CompanyNews");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsUser_AspNetUsers_authorId",
                table: "NewsUser");

            migrationBuilder.DropForeignKey(
                name: "FK_pollutions_pollutants_rfc_factor_id",
                table: "pollutions");

            migrationBuilder.DropForeignKey(
                name: "FK_tax_norms_pollutants_rfc_factor_id",
                table: "tax_norms");

            migrationBuilder.RenameColumn(
                name: "rfc_factor_id",
                table: "tax_norms",
                newName: "pollutant_id");

            migrationBuilder.RenameColumn(
                name: "factor_Name",
                table: "tax_norms",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_tax_norms_year_factor_Name",
                table: "tax_norms",
                newName: "IX_tax_norms_year_name");

            migrationBuilder.RenameIndex(
                name: "IX_tax_norms_rfc_factor_id",
                table: "tax_norms",
                newName: "IX_tax_norms_pollutant_id");

            migrationBuilder.RenameColumn(
                name: "rfc_factor_id",
                table: "pollutions",
                newName: "pollutant_id");

            migrationBuilder.RenameColumn(
                name: "factor_value",
                table: "pollutions",
                newName: "pollution_value");

            migrationBuilder.RenameColumn(
                name: "factor_Name",
                table: "pollutions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "factor_Ch_value",
                table: "pollutions",
                newName: "CH_value");

            migrationBuilder.RenameColumn(
                name: "factor_Ca_value",
                table: "pollutions",
                newName: "CA_value");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_rfc_factor_id",
                table: "pollutions",
                newName: "IX_pollutions_pollutant_id");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_factor_Name_passport_id",
                table: "pollutions",
                newName: "IX_pollutions_name_passport_id");

            migrationBuilder.RenameColumn(
                name: "factor_value",
                table: "pollutants",
                newName: "RFC_value");

            migrationBuilder.RenameColumn(
                name: "factor_Name",
                table: "pollutants",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_pollutants_factor_Name",
                table: "pollutants",
                newName: "IX_pollutants_name");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "NewsUser",
                newName: "authorsId");

            migrationBuilder.RenameColumn(
                name: "companyid",
                table: "CompanyNews",
                newName: "companiesid");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNews_companies_companiesid",
                table: "CompanyNews",
                column: "companiesid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsUser_AspNetUsers_authorsId",
                table: "NewsUser",
                column: "authorsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pollutions_pollutants_pollutant_id",
                table: "pollutions",
                column: "pollutant_id",
                principalTable: "pollutants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tax_norms_pollutants_pollutant_id",
                table: "tax_norms",
                column: "pollutant_id",
                principalTable: "pollutants",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyNews_companies_companiesid",
                table: "CompanyNews");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsUser_AspNetUsers_authorsId",
                table: "NewsUser");

            migrationBuilder.DropForeignKey(
                name: "FK_pollutions_pollutants_pollutant_id",
                table: "pollutions");

            migrationBuilder.DropForeignKey(
                name: "FK_tax_norms_pollutants_pollutant_id",
                table: "tax_norms");

            migrationBuilder.RenameColumn(
                name: "pollutant_id",
                table: "tax_norms",
                newName: "rfc_factor_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tax_norms",
                newName: "factor_Name");

            migrationBuilder.RenameIndex(
                name: "IX_tax_norms_year_name",
                table: "tax_norms",
                newName: "IX_tax_norms_year_factor_Name");

            migrationBuilder.RenameIndex(
                name: "IX_tax_norms_pollutant_id",
                table: "tax_norms",
                newName: "IX_tax_norms_rfc_factor_id");

            migrationBuilder.RenameColumn(
                name: "pollution_value",
                table: "pollutions",
                newName: "factor_value");

            migrationBuilder.RenameColumn(
                name: "pollutant_id",
                table: "pollutions",
                newName: "rfc_factor_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "pollutions",
                newName: "factor_Name");

            migrationBuilder.RenameColumn(
                name: "CH_value",
                table: "pollutions",
                newName: "factor_Ch_value");

            migrationBuilder.RenameColumn(
                name: "CA_value",
                table: "pollutions",
                newName: "factor_Ca_value");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_pollutant_id",
                table: "pollutions",
                newName: "IX_pollutions_rfc_factor_id");

            migrationBuilder.RenameIndex(
                name: "IX_pollutions_name_passport_id",
                table: "pollutions",
                newName: "IX_pollutions_factor_Name_passport_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "pollutants",
                newName: "factor_Name");

            migrationBuilder.RenameColumn(
                name: "RFC_value",
                table: "pollutants",
                newName: "factor_value");

            migrationBuilder.RenameIndex(
                name: "IX_pollutants_name",
                table: "pollutants",
                newName: "IX_pollutants_factor_Name");

            migrationBuilder.RenameColumn(
                name: "authorsId",
                table: "NewsUser",
                newName: "authorId");

            migrationBuilder.RenameColumn(
                name: "companiesid",
                table: "CompanyNews",
                newName: "companyid");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyNews_companies_companyid",
                table: "CompanyNews",
                column: "companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsUser_AspNetUsers_authorId",
                table: "NewsUser",
                column: "authorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pollutions_pollutants_rfc_factor_id",
                table: "pollutions",
                column: "rfc_factor_id",
                principalTable: "pollutants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tax_norms_pollutants_rfc_factor_id",
                table: "tax_norms",
                column: "rfc_factor_id",
                principalTable: "pollutants",
                principalColumn: "id");
        }
    }
}
