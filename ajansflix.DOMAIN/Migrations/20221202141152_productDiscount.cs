using Microsoft.EntityFrameworkCore.Migrations;

namespace ajansflix.DOMAIN.Migrations
{
    public partial class productDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
