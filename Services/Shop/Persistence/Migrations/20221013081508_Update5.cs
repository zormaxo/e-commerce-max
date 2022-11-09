using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations;

public partial class Update5 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(name: "Username", table: "Users", newName: "UserName");

        migrationBuilder.RenameColumn(name: "Surname", table: "Users", newName: "LastName");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(name: "UserName", table: "Users", newName: "Username");

        migrationBuilder.RenameColumn(name: "LastName", table: "Users", newName: "Surname");
    }
}
