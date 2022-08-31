using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Data.Migrations
{
    public partial class ProductMachine3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMachine_Products_Id",
                table: "ProductMachine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMachine",
                table: "ProductMachine");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductMachine");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "ProductMachine");

            migrationBuilder.RenameTable(
                name: "ProductMachine",
                newName: "ProductMachines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMachines",
                table: "ProductMachines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMachines_Products_Id",
                table: "ProductMachines",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMachines_Products_Id",
                table: "ProductMachines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMachines",
                table: "ProductMachines");

            migrationBuilder.RenameTable(
                name: "ProductMachines",
                newName: "ProductMachine");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductMachine",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "ProductMachine",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMachine",
                table: "ProductMachine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMachine_Products_Id",
                table: "ProductMachine",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
