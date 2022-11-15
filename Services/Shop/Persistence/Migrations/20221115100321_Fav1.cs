using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fav1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMachines_Products_Id",
                table: "ProductMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Products_Id",
                table: "ProductMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Favourite_FavouriteId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FavouriteId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Favourite",
                newName: "Favourites");

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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductMachines",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductMachines",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikedProductId",
                table: "Favourites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMachines_ProductId",
                table: "ProductMachines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_LikedProductId",
                table: "Favourites",
                column: "LikedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Products_LikedProductId",
                table: "Favourites",
                column: "LikedProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Users_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMachines_Products_ProductId",
                table: "ProductMachines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Products_ProductId",
                table: "ProductMaterials",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Products_LikedProductId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Users_UserId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMachines_Products_ProductId",
                table: "ProductMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Products_ProductId",
                table: "ProductMaterials");

            migrationBuilder.DropIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials");

            migrationBuilder.DropIndex(
                name: "IX_ProductMachines_ProductId",
                table: "ProductMachines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_LikedProductId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductMaterials");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductMachines");

            migrationBuilder.DropColumn(
                name: "LikedProductId",
                table: "Favourites");

            migrationBuilder.RenameTable(
                name: "Favourites",
                newName: "Favourite");

            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductMaterials",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductMachines",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FavouriteId",
                table: "Products",
                column: "FavouriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMachines_Products_Id",
                table: "ProductMachines",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Products_Id",
                table: "ProductMaterials",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Favourite_FavouriteId",
                table: "Products",
                column: "FavouriteId",
                principalTable: "Favourite",
                principalColumn: "Id");
        }
    }
}
