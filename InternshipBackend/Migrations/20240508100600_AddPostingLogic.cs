using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPostingLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_AdminUserId",
                schema: "public",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "IsVertified",
                schema: "public",
                table: "Companies",
                newName: "IsVerified");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "Companies",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundPhotoUrl",
                schema: "public",
                table: "Companies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                schema: "public",
                table: "Companies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                schema: "public",
                table: "Companies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                schema: "public",
                table: "Companies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sector",
                schema: "public",
                table: "Companies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                schema: "public",
                table: "Companies",
                type: "character varying(75)",
                maxLength: 75,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                schema: "public",
                table: "Companies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InternshipPosting",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Sector = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    Location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Requirements = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WorkType = table.Column<int>(type: "integer", nullable: false),
                    EmploymentType = table.Column<int>(type: "integer", nullable: false),
                    HasSalary = table.Column<bool>(type: "boolean", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipPosting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipPosting_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "public",
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipPosting_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "public",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipPosting_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "public",
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternshipApplication",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InternshipPostingId = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CvUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipApplication_InternshipPosting_InternshipPostingId",
                        column: x => x.InternshipPostingId,
                        principalSchema: "public",
                        principalTable: "InternshipPosting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipApplication_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AdminUserId",
                schema: "public",
                table: "Companies",
                column: "AdminUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                schema: "public",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                schema: "public",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipApplication_InternshipPostingId",
                schema: "public",
                table: "InternshipApplication",
                column: "InternshipPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipApplication_UserId",
                schema: "public",
                table: "InternshipApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPosting_CityId",
                schema: "public",
                table: "InternshipPosting",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPosting_CompanyId",
                schema: "public",
                table: "InternshipPosting",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPosting_CountryId",
                schema: "public",
                table: "InternshipPosting",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Cities_CityId",
                schema: "public",
                table: "Companies",
                column: "CityId",
                principalSchema: "public",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Countries_CountryId",
                schema: "public",
                table: "Companies",
                column: "CountryId",
                principalSchema: "public",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Cities_CityId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Countries_CountryId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "InternshipApplication",
                schema: "public");

            migrationBuilder.DropTable(
                name: "InternshipPosting",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AdminUserId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CityId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CountryId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BackgroundPhotoUrl",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Sector",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                schema: "public",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                schema: "public",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                schema: "public",
                table: "Companies",
                newName: "IsVertified");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "Companies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AdminUserId",
                schema: "public",
                table: "Companies",
                column: "AdminUserId");
        }
    }
}
