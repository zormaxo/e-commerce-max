using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Products_ProductId",
                table: "ProductMaterials");

            migrationBuilder.DropIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductMaterials");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductMaterials",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "MainCategory",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Products_Id",
                table: "ProductMaterials",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Products_Id",
                table: "ProductMaterials");

            migrationBuilder.DropColumn(
                name: "MainCategory",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductMaterials",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductMaterials",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Products_ProductId",
                table: "ProductMaterials",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
