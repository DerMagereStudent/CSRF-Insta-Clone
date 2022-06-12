using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSRFInstaClone.WebAPI.Migrations
{
    public partial class LikePostOneToManyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Likes_PostId",
                table: "Likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId",
                unique: true);
        }
    }
}
