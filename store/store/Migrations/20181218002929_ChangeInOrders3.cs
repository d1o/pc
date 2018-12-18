using Microsoft.EntityFrameworkCore.Migrations;

namespace store.Migrations
{
    public partial class ChangeInOrders3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingCost",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShippingCost",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
