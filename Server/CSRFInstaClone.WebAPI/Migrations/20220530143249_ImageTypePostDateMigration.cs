using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSRFInstaClone.WebAPI.Migrations
{
    public partial class ImageTypePostDateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimePosted",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimePosted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Images");
        }
    }
}
