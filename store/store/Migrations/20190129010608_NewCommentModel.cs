using Microsoft.EntityFrameworkCore.Migrations;

namespace store.Migrations
{
    public partial class NewCommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductAuthor",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAuthor",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Comments");
        }
    }
}
