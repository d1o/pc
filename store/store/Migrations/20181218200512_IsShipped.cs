using Microsoft.EntityFrameworkCore.Migrations;

namespace store.Migrations
{
    public partial class IsShipped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ShippingCost",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingCost",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
