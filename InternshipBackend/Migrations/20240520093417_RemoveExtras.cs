﻿using InternshipBackend.Data.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extras",
                schema: "public",
                table: "UserDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<UserDetailExtras>(
                name: "Extras",
                schema: "public",
                table: "UserDetails",
                type: "jsonb",
                nullable: true);
        }
    }
}
