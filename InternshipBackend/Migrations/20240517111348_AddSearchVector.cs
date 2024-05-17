using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchVector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                schema: "public",
                table: "InternshipPostings",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "turkish")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPostings_SearchVector",
                schema: "public",
                table: "InternshipPostings",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InternshipPostings_SearchVector",
                schema: "public",
                table: "InternshipPostings");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "public",
                table: "InternshipPostings");
        }
    }
}
