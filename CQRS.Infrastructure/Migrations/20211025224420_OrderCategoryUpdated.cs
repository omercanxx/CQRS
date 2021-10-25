using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRS.Infrastructure.Migrations
{
    public partial class OrderCategoryUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prdoduct_Campaigns_Campaigns_CampaignId",
                table: "Prdoduct_Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Prdoduct_Campaigns_Orders_OrderId",
                table: "Prdoduct_Campaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prdoduct_Campaigns",
                table: "Prdoduct_Campaigns");

            migrationBuilder.RenameTable(
                name: "Prdoduct_Campaigns",
                newName: "Order_Campaigns");

            migrationBuilder.RenameIndex(
                name: "IX_Prdoduct_Campaigns_OrderId",
                table: "Order_Campaigns",
                newName: "IX_Order_Campaigns_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Prdoduct_Campaigns_CampaignId",
                table: "Order_Campaigns",
                newName: "IX_Order_Campaigns_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Campaigns",
                table: "Order_Campaigns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Campaigns_Campaigns_CampaignId",
                table: "Order_Campaigns",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Campaigns_Orders_OrderId",
                table: "Order_Campaigns",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Campaigns_Campaigns_CampaignId",
                table: "Order_Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Campaigns_Orders_OrderId",
                table: "Order_Campaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Campaigns",
                table: "Order_Campaigns");

            migrationBuilder.RenameTable(
                name: "Order_Campaigns",
                newName: "Prdoduct_Campaigns");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Campaigns_OrderId",
                table: "Prdoduct_Campaigns",
                newName: "IX_Prdoduct_Campaigns_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Campaigns_CampaignId",
                table: "Prdoduct_Campaigns",
                newName: "IX_Prdoduct_Campaigns_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prdoduct_Campaigns",
                table: "Prdoduct_Campaigns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prdoduct_Campaigns_Campaigns_CampaignId",
                table: "Prdoduct_Campaigns",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prdoduct_Campaigns_Orders_OrderId",
                table: "Prdoduct_Campaigns",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
