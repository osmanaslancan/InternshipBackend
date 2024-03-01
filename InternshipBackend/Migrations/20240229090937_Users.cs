using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_UserInfos_AdminUserUserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_UserInfos_UserId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ForeignLanguages_UserInfos_UserId",
                table: "ForeignLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UniversityEducations_UserInfos_UserId",
                table: "UniversityEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_UserInfos_UserId",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_UserInfos_UserId",
                table: "UserProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkHistories_UserInfos_UserId",
                table: "WorkHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_SupabaseId",
                table: "Users",
                newName: "IX_Users_SupabaseId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_AdminUserUserId",
                table: "Companies",
                column: "AdminUserUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Users_UserId",
                table: "CompanyEmployees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignLanguages_Users_UserId",
                table: "ForeignLanguages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UniversityEducations_Users_UserId",
                table: "UniversityEducations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users_UserId",
                table: "UserDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHistories_Users_UserId",
                table: "WorkHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_AdminUserUserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Users_UserId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ForeignLanguages_Users_UserId",
                table: "ForeignLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UniversityEducations_Users_UserId",
                table: "UniversityEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Users_UserId",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkHistories_Users_UserId",
                table: "WorkHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserInfos");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SupabaseId",
                table: "UserInfos",
                newName: "IX_UserInfos_SupabaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "UserInfos",
                newName: "IX_UserInfos_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_UserInfos_AdminUserUserId",
                table: "Companies",
                column: "AdminUserUserId",
                principalTable: "UserInfos",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_UserInfos_UserId",
                table: "CompanyEmployees",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignLanguages_UserInfos_UserId",
                table: "ForeignLanguages",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UniversityEducations_UserInfos_UserId",
                table: "UniversityEducations",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_UserInfos_UserId",
                table: "UserDetails",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_UserInfos_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHistories_UserInfos_UserId",
                table: "WorkHistories",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
