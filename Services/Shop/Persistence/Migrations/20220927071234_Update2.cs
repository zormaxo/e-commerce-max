using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations;

public partial class Update2 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ProductMaterials",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                IsNew = table.Column<bool>(type: "INTEGER", nullable: false),
                ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductMaterials", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductMaterials_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(name: "IX_ProductMaterials_ProductId", table: "ProductMaterials", column: "ProductId");
    }

    protected override void Down(MigrationBuilder migrationBuilder) { migrationBuilder.DropTable(name: "ProductMaterials"); }
}
