﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTestTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Doctors");
        }
    }
}
