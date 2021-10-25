using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRS.Infrastructure.Migrations
{
    public partial class OrderCategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prdoduct_Campaigns_Products_ProductId",
                table: "Prdoduct_Campaigns");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Prdoduct_Campaigns",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Prdoduct_Campaigns_ProductId",
                table: "Prdoduct_Campaigns",
                newName: "IX_Prdoduct_Campaigns_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order_Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Prdoduct_Campaigns_Orders_OrderId",
                table: "Prdoduct_Campaigns",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prdoduct_Campaigns_Orders_OrderId",
                table: "Prdoduct_Campaigns");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order_Products");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Prdoduct_Campaigns",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Prdoduct_Campaigns_OrderId",
                table: "Prdoduct_Campaigns",
                newName: "IX_Prdoduct_Campaigns_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prdoduct_Campaigns_Products_ProductId",
                table: "Prdoduct_Campaigns",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
