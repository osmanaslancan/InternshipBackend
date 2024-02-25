using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class CountryCode3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code3",
                table: "Countries",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code3",
                table: "Countries");
        }
    }
}
