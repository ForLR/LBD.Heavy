using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heavy.Identity.Migrations
{
    public partial class _201911261515 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ClaimTypes",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ClaimTypes",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
