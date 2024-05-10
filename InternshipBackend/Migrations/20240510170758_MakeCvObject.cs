using System.Collections.Generic;
using InternshipBackend.Data.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class MakeCvObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Cvs", "UserDetails", "public");
            migrationBuilder.AddColumn<List<UserCv>>(
                name: "Cvs",
                schema: "public",
                table: "UserDetails",
                type: "jsonb",
                defaultValue: new List<UserCv>(),
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Cvs", "UserDetails", "public");
            migrationBuilder.AddColumn<List<string>>(
                name: "Cvs",
                schema: "public",
                table: "UserDetails",
                type: "text[]",
                nullable: false);
        }
    }
}
