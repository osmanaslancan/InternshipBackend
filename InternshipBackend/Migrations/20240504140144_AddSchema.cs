using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "WorkHistories",
                newName: "WorkHistories",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "UserProjects",
                newName: "UserProjects",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "UserPermission",
                newName: "UserPermission",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                newName: "UserDetails",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "UniversityEducations",
                newName: "UniversityEducations",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "Universities",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "ForeignLanguages",
                newName: "ForeignLanguages",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "DriverLicenses",
                newName: "DriverLicenses",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "DbSeeds",
                newName: "DbSeeds",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Countries",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "CompanyEmployees",
                newName: "CompanyEmployees",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Companies",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "Cities",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WorkHistories",
                schema: "public",
                newName: "WorkHistories");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "public",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserProjects",
                schema: "public",
                newName: "UserProjects");

            migrationBuilder.RenameTable(
                name: "UserPermission",
                schema: "public",
                newName: "UserPermission");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                schema: "public",
                newName: "UserDetails");

            migrationBuilder.RenameTable(
                name: "UniversityEducations",
                schema: "public",
                newName: "UniversityEducations");

            migrationBuilder.RenameTable(
                name: "Universities",
                schema: "public",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "ForeignLanguages",
                schema: "public",
                newName: "ForeignLanguages");

            migrationBuilder.RenameTable(
                name: "DriverLicenses",
                schema: "public",
                newName: "DriverLicenses");

            migrationBuilder.RenameTable(
                name: "DbSeeds",
                schema: "public",
                newName: "DbSeeds");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "public",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "CompanyEmployees",
                schema: "public",
                newName: "CompanyEmployees");

            migrationBuilder.RenameTable(
                name: "Companies",
                schema: "public",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "public",
                newName: "Cities");
        }
    }
}
