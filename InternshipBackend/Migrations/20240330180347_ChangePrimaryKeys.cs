using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ForeignLanguages",
                table: "ForeignLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEmployees",
                table: "CompanyEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ForeignLanguages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CompanyEmployees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForeignLanguages",
                table: "ForeignLanguages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEmployees",
                table: "CompanyEmployees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignLanguages_UserId",
                table: "ForeignLanguages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_UserId",
                table: "CompanyEmployees",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ForeignLanguages",
                table: "ForeignLanguages");

            migrationBuilder.DropIndex(
                name: "IX_ForeignLanguages_UserId",
                table: "ForeignLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEmployees",
                table: "CompanyEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CompanyEmployees_UserId",
                table: "CompanyEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ForeignLanguages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CompanyEmployees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForeignLanguages",
                table: "ForeignLanguages",
                columns: new[] { "UserId", "LanguageCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEmployees",
                table: "CompanyEmployees",
                columns: new[] { "UserId", "CompanyId" });
        }
    }
}
