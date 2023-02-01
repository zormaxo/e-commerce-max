using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Shop.Persistence.Migrations;

/// <inheritdoc/>
public partial class Update1 : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "LastActive",
            table: "AspNetUsers",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "LastActive",
            table: "AspNetUsers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);
    }
}
