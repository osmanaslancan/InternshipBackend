using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipApplication_InternshipPosting_InternshipPostingId",
                schema: "public",
                table: "InternshipApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPosting_Cities_CityId",
                schema: "public",
                table: "InternshipPosting");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPosting_Companies_CompanyId",
                schema: "public",
                table: "InternshipPosting");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPosting_Countries_CountryId",
                schema: "public",
                table: "InternshipPosting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipPosting",
                schema: "public",
                table: "InternshipPosting");

            migrationBuilder.RenameTable(
                name: "InternshipPosting",
                schema: "public",
                newName: "InternshipPostings",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPosting_CountryId",
                schema: "public",
                table: "InternshipPostings",
                newName: "IX_InternshipPostings_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPosting_CompanyId",
                schema: "public",
                table: "InternshipPostings",
                newName: "IX_InternshipPostings_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPosting_CityId",
                schema: "public",
                table: "InternshipPostings",
                newName: "IX_InternshipPostings_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipPostings",
                schema: "public",
                table: "InternshipPostings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipApplication_InternshipPostings_InternshipPostingId",
                schema: "public",
                table: "InternshipApplication",
                column: "InternshipPostingId",
                principalSchema: "public",
                principalTable: "InternshipPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPostings_Cities_CityId",
                schema: "public",
                table: "InternshipPostings",
                column: "CityId",
                principalSchema: "public",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPostings_Companies_CompanyId",
                schema: "public",
                table: "InternshipPostings",
                column: "CompanyId",
                principalSchema: "public",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPostings_Countries_CountryId",
                schema: "public",
                table: "InternshipPostings",
                column: "CountryId",
                principalSchema: "public",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipApplication_InternshipPostings_InternshipPostingId",
                schema: "public",
                table: "InternshipApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPostings_Cities_CityId",
                schema: "public",
                table: "InternshipPostings");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPostings_Companies_CompanyId",
                schema: "public",
                table: "InternshipPostings");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPostings_Countries_CountryId",
                schema: "public",
                table: "InternshipPostings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipPostings",
                schema: "public",
                table: "InternshipPostings");

            migrationBuilder.RenameTable(
                name: "InternshipPostings",
                schema: "public",
                newName: "InternshipPosting",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPostings_CountryId",
                schema: "public",
                table: "InternshipPosting",
                newName: "IX_InternshipPosting_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPostings_CompanyId",
                schema: "public",
                table: "InternshipPosting",
                newName: "IX_InternshipPosting_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPostings_CityId",
                schema: "public",
                table: "InternshipPosting",
                newName: "IX_InternshipPosting_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipPosting",
                schema: "public",
                table: "InternshipPosting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipApplication_InternshipPosting_InternshipPostingId",
                schema: "public",
                table: "InternshipApplication",
                column: "InternshipPostingId",
                principalSchema: "public",
                principalTable: "InternshipPosting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPosting_Cities_CityId",
                schema: "public",
                table: "InternshipPosting",
                column: "CityId",
                principalSchema: "public",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPosting_Companies_CompanyId",
                schema: "public",
                table: "InternshipPosting",
                column: "CompanyId",
                principalSchema: "public",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPosting_Countries_CountryId",
                schema: "public",
                table: "InternshipPosting",
                column: "CountryId",
                principalSchema: "public",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
