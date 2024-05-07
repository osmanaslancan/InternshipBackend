using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDriverLicenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverLicenses",
                schema: "public");

            migrationBuilder.AddColumn<string[]>(
                name: "DriverLicenses",
                schema: "public",
                table: "UserDetails",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverLicenses",
                schema: "public",
                table: "UserDetails");

            migrationBuilder.CreateTable(
                name: "DriverLicenses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    License = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserDetailId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverLicenses_UserDetails_UserDetailId",
                        column: x => x.UserDetailId,
                        principalSchema: "public",
                        principalTable: "UserDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverLicenses_UserDetailId",
                schema: "public",
                table: "DriverLicenses",
                column: "UserDetailId");
        }
    }
}
