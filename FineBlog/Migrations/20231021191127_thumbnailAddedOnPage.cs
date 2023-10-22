using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FineBlog.Migrations
{
    public partial class thumbnailAddedOnPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Posts",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "title");
        }
    }
}
