using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Disco.Migrations
{
    public partial class mod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateTime",
                schema: "dbo",
                table: "IdentiyCard");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "dbo",
                table: "IdentiyCard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "dbo",
                table: "IdentiyCard");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateTime",
                schema: "dbo",
                table: "IdentiyCard",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                schema: "dbo",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
