using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Disco.Migrations
{
    public partial class mod3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackList",
                schema: "dbo",
                columns: table => new
                {
                    BlackId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(nullable: true),
                    BlackListDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackList", x => x.BlackId);
                    table.ForeignKey(
                        name: "FK_BlackList_Member_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "dbo",
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_MemberId",
                schema: "dbo",
                table: "BlackList",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackList",
                schema: "dbo");
        }
    }
}
