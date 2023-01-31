using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookswap.Domain.Migrations
{
    public partial class Covers_BookRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Covers",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Covers_BookId",
                table: "Covers",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_Books_BookId",
                table: "Covers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_Books_BookId",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_BookId",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Covers");
        }
    }
}
