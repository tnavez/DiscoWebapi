using Microsoft.EntityFrameworkCore.Migrations;

namespace Disco.Migrations
{
    public partial class MigrationName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                schema: "dbo",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_MemberId",
                schema: "dbo",
                table: "Cards",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Member_MemberId",
                schema: "dbo",
                table: "Cards",
                column: "MemberId",
                principalSchema: "dbo",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Member_MemberId",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_MemberId",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "MemberId",
                schema: "dbo",
                table: "Cards");
        }
    }
}
