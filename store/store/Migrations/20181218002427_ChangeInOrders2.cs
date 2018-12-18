using Microsoft.EntityFrameworkCore.Migrations;

namespace store.Migrations
{
    public partial class ChangeInOrders2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShippingCost",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingCost",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
