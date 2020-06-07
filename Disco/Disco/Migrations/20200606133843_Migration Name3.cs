using Microsoft.EntityFrameworkCore.Migrations;

namespace Disco.Migrations
{
    public partial class MigrationName3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Member_MemberId",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                schema: "dbo",
                table: "IdentiyCard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                schema: "dbo",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentiyCard_MemberId",
                schema: "dbo",
                table: "IdentiyCard",
                column: "MemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Member_MemberId",
                schema: "dbo",
                table: "Cards",
                column: "MemberId",
                principalSchema: "dbo",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentiyCard_Member_MemberId",
                schema: "dbo",
                table: "IdentiyCard",
                column: "MemberId",
                principalSchema: "dbo",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Member_MemberId",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentiyCard_Member_MemberId",
                schema: "dbo",
                table: "IdentiyCard");

            migrationBuilder.DropIndex(
                name: "IX_IdentiyCard_MemberId",
                schema: "dbo",
                table: "IdentiyCard");

            migrationBuilder.DropColumn(
                name: "MemberId",
                schema: "dbo",
                table: "IdentiyCard");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                schema: "dbo",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
