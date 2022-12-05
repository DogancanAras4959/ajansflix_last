using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ajansflix.DOMAIN.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetails_orders_OrderId",
                table: "productDetails");

            migrationBuilder.DropColumn(
                name: "ComponentPrice",
                table: "productDetails");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "productDetails",
                newName: "DetailId");

            migrationBuilder.RenameIndex(
                name: "IX_productDetails_OrderId",
                table: "productDetails",
                newName: "IX_productDetails_DetailId");

            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_productDetails_details_DetailId",
                table: "productDetails",
                column: "DetailId",
                principalTable: "details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetails_details_DetailId",
                table: "productDetails");

            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.RenameColumn(
                name: "DetailId",
                table: "productDetails",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_productDetails_DetailId",
                table: "productDetails",
                newName: "IX_productDetails_OrderId");

            migrationBuilder.AddColumn<decimal>(
                name: "ComponentPrice",
                table: "productDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_productDetails_orders_OrderId",
                table: "productDetails",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
