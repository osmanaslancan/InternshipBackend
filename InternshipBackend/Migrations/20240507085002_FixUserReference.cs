using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixUserReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReference_UserDetails_UserDetailId",
                schema: "public",
                table: "UserReference");

            migrationBuilder.DropIndex(
                name: "IX_UserReference_UserDetailId",
                schema: "public",
                table: "UserReference");

            migrationBuilder.DropColumn(
                name: "UserDetailId",
                schema: "public",
                table: "UserReference");

            migrationBuilder.CreateIndex(
                name: "IX_UserReference_UserId",
                schema: "public",
                table: "UserReference",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReference_UserDetails_UserId",
                schema: "public",
                table: "UserReference",
                column: "UserId",
                principalSchema: "public",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReference_Users_UserId",
                schema: "public",
                table: "UserReference",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReference_UserDetails_UserId",
                schema: "public",
                table: "UserReference");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReference_Users_UserId",
                schema: "public",
                table: "UserReference");

            migrationBuilder.DropIndex(
                name: "IX_UserReference_UserId",
                schema: "public",
                table: "UserReference");

            migrationBuilder.AddColumn<int>(
                name: "UserDetailId",
                schema: "public",
                table: "UserReference",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReference_UserDetailId",
                schema: "public",
                table: "UserReference",
                column: "UserDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReference_UserDetails_UserDetailId",
                schema: "public",
                table: "UserReference",
                column: "UserDetailId",
                principalSchema: "public",
                principalTable: "UserDetails",
                principalColumn: "Id");
        }
    }
}
