using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ajansflix.DOMAIN.Migrations
{
    public partial class databaseChangedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactDatas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "customers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "customers",
                newName: "Messagess");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "orderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_StatusId",
                table: "orders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderStatus_StatusId",
                table: "orders",
                column: "StatusId",
                principalTable: "orderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderStatus_StatusId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "orderStatus");

            migrationBuilder.DropIndex(
                name: "IX_orders_StatusId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "customers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Messagess",
                table: "customers",
                newName: "NameSurname");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "contactDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contactDatas_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contactDatas_CustomerId",
                table: "contactDatas",
                column: "CustomerId");
        }
    }
}
