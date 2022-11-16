using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Persistence.Migrations;

/// <inheritdoc/>
public partial class Favourite : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(name: "FavouriteId", table: "Products", type: "INTEGER", nullable: true);

        migrationBuilder.CreateTable(
            name: "Favourite",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Favourite", x => x.Id);
            });

        migrationBuilder.CreateIndex(name: "IX_Products_FavouriteId", table: "Products", column: "FavouriteId");

        migrationBuilder.AddForeignKey(
            name: "FK_Products_Favourite_FavouriteId",
            table: "Products",
            column: "FavouriteId",
            principalTable: "Favourite",
            principalColumn: "Id");
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(name: "FK_Products_Favourite_FavouriteId", table: "Products");

        migrationBuilder.DropTable(name: "Favourite");

        migrationBuilder.DropIndex(name: "IX_Products_FavouriteId", table: "Products");

        migrationBuilder.DropColumn(name: "FavouriteId", table: "Products");
    }
}
