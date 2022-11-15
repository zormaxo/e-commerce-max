using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations;

public partial class Update4 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(name: "FK_Products_ProductBrands_ProductBrandId", table: "Products");

        migrationBuilder.DropIndex(name: "IX_Products_ProductBrandId", table: "Products");

        migrationBuilder.DropColumn(name: "ProductBrandId", table: "Products");

        migrationBuilder.DropColumn(name: "Count", table: "Categories");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ProductBrandId",
            table: "Products",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(name: "Count", table: "Categories", type: "INTEGER", nullable: false, defaultValue: 0);

        migrationBuilder.CreateIndex(name: "IX_Products_ProductBrandId", table: "Products", column: "ProductBrandId");

        migrationBuilder.AddForeignKey(
            name: "FK_Products_ProductBrands_ProductBrandId",
            table: "Products",
            column: "ProductBrandId",
            principalTable: "ProductBrands",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
