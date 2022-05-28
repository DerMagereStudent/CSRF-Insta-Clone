using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSRFInstaClone.WebAPI.Migrations
{
    public partial class ImageDataTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "UserProfiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "UserProfiles",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
