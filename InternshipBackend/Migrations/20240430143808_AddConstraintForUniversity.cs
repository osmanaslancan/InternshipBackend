using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintForUniversity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UniversityEducations_UniversityId",
                table: "UniversityEducations",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UniversityEducations_Universities_UniversityId",
                table: "UniversityEducations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UniversityEducations_Universities_UniversityId",
                table: "UniversityEducations");

            migrationBuilder.DropIndex(
                name: "IX_UniversityEducations_UniversityId",
                table: "UniversityEducations");
        }
    }
}
