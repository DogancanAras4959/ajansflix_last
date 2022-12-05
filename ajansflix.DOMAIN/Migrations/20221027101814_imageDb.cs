using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ajansflix.DOMAIN.Migrations
{
    public partial class imageDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imageDatas");

            migrationBuilder.CreateTable(
                name: "imagedata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FullscreenContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagedata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "imagefile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagefile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quantityComponent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompQuantity = table.Column<int>(type: "int", nullable: false),
                    CompPrice = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CompDeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quantityComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quantityComponent_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_quantityComponent_ProductId",
                table: "quantityComponent",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagedata");

            migrationBuilder.DropTable(
                name: "imagefile");

            migrationBuilder.DropTable(
                name: "quantityComponent");

            migrationBuilder.CreateTable(
                name: "imageDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullscreenContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OriginalContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imageDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_imageDatas_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_imageDatas_ProductId",
                table: "imageDatas",
                column: "ProductId");
        }
    }
}
