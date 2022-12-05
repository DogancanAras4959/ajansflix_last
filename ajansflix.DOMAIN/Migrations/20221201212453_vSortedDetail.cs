using Microsoft.EntityFrameworkCore.Migrations;

namespace ajansflix.DOMAIN.Migrations
{
    public partial class vSortedDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sorted",
                table: "productDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sorted",
                table: "productDetails");
        }
    }
}
