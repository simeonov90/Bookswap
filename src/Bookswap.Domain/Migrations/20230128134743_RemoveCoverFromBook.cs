using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookswap.Domain.Migrations
{
    public partial class RemoveCoverFromBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Covers_CoverId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CoverId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoverId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Books_CoverId",
                table: "Books",
                column: "CoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Covers_CoverId",
                table: "Books",
                column: "CoverId",
                principalTable: "Covers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
