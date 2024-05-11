using System.Collections.Generic;
using InternshipBackend.Data.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cvs",
                schema: "public",
                table: "UserDetails",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<UserCv>),
                oldType: "jsonb");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                schema: "public",
                table: "InternshipPostings",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                schema: "public",
                table: "InternshipPostings");

            migrationBuilder.AlterColumn<List<UserCv>>(
                name: "Cvs",
                schema: "public",
                table: "UserDetails",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
