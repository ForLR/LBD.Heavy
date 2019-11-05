using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heavy.Data.Migrations
{
    public partial class AblumInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "album",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    create_date_time = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    title = table.Column<string>(maxLength: 20, nullable: true),
                    img_url = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_album", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "album");
        }
    }
}
