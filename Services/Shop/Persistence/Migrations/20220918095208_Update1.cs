using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Infrastructure.Migrations;

public partial class Update1 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Audits",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<int>(type: "INTEGER", nullable: false),
                Action = table.Column<string>(type: "TEXT", nullable: true),
                TableName = table.Column<string>(type: "TEXT", nullable: true),
                DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                KeyValues = table.Column<string>(type: "TEXT", nullable: true),
                OldValues = table.Column<string>(type: "TEXT", nullable: true),
                NewValues = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Audits", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Count = table.Column<int>(type: "INTEGER", nullable: false),
                ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                Url = table.Column<string>(type: "TEXT", nullable: true),
                CanBeAdded = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
                table.ForeignKey(
                    name: "FK_Categories_Categories_ParentId",
                    column: x => x.ParentId,
                    principalTable: "Categories",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Cities",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cities", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Currency",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                Try = table.Column<double>(type: "REAL", nullable: false),
                Usd = table.Column<double>(type: "REAL", nullable: false),
                Eur = table.Column<double>(type: "REAL", nullable: false),
                Gbp = table.Column<double>(type: "REAL", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Currency", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProductBrands",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductBrands", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Username = table.Column<string>(type: "TEXT", nullable: false),
                FirstName = table.Column<string>(type: "TEXT", nullable: false),
                Surname = table.Column<string>(type: "TEXT", nullable: false),
                LogoUrl = table.Column<string>(type: "TEXT", nullable: true),
                PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false),
                LastActive = table.Column<DateTime>(type: "TEXT", nullable: false),
                IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                ModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Counties",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                CityId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Counties", x => x.Id);
                table.ForeignKey(
                    name: "FK_Counties_Cities_CityId",
                    column: x => x.CityId,
                    principalTable: "Cities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserPhotos",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Url = table.Column<string>(type: "TEXT", nullable: false),
                IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                PublicId = table.Column<string>(type: "TEXT", nullable: true),
                UserId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserPhotos", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserPhotos_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: false),
                Price = table.Column<double>(type: "decimal(18,2)", nullable: false),
                CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                ProductBrandId = table.Column<int>(type: "INTEGER", nullable: false),
                UserId = table.Column<int>(type: "INTEGER", nullable: false),
                CountyId = table.Column<int>(type: "INTEGER", nullable: false),
                Showcase = table.Column<bool>(type: "INTEGER", nullable: false),
                IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                Currency = table.Column<int>(type: "INTEGER", nullable: false),
                IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                ModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    name: "FK_Products_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Products_Counties_CountyId",
                    column: x => x.CountyId,
                    principalTable: "Counties",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Products_ProductBrands_ProductBrandId",
                    column: x => x.ProductBrandId,
                    principalTable: "ProductBrands",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Products_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProductMachines",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false),
                IsNew = table.Column<bool>(type: "INTEGER", nullable: false),
                IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductMachines", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductMachines_Products_Id",
                    column: x => x.Id,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProductPhotos",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Url = table.Column<string>(type: "TEXT", nullable: false),
                IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                PublicId = table.Column<string>(type: "TEXT", nullable: true),
                ProductId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductPhotos", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductPhotos_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(name: "IX_Categories_Name", table: "Categories", column: "Name", unique: true);

        migrationBuilder.CreateIndex(name: "IX_Categories_ParentId", table: "Categories", column: "ParentId");

        migrationBuilder.CreateIndex(name: "IX_Counties_CityId", table: "Counties", column: "CityId");

        migrationBuilder.CreateIndex(name: "IX_ProductPhotos_ProductId", table: "ProductPhotos", column: "ProductId");

        migrationBuilder.CreateIndex(name: "IX_Products_CategoryId", table: "Products", column: "CategoryId");

        migrationBuilder.CreateIndex(name: "IX_Products_CountyId", table: "Products", column: "CountyId");

        migrationBuilder.CreateIndex(name: "IX_Products_ProductBrandId", table: "Products", column: "ProductBrandId");

        migrationBuilder.CreateIndex(name: "IX_Products_UserId", table: "Products", column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_UserPhotos_UserId", table: "UserPhotos", column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Audits");

        migrationBuilder.DropTable(name: "Currency");

        migrationBuilder.DropTable(name: "ProductMachines");

        migrationBuilder.DropTable(name: "ProductPhotos");

        migrationBuilder.DropTable(name: "UserPhotos");

        migrationBuilder.DropTable(name: "Products");

        migrationBuilder.DropTable(name: "Categories");

        migrationBuilder.DropTable(name: "Counties");

        migrationBuilder.DropTable(name: "ProductBrands");

        migrationBuilder.DropTable(name: "Users");

        migrationBuilder.DropTable(name: "Cities");
    }
}
