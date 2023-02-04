﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp.NetCoreMVCCoding.Migrations
{
    public partial class addProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageFileName",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true, defaultValue:"no-image.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageFileName",
                table: "Users");
        }
    }
}
