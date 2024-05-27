using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Migrations
{
    /// <inheritdoc />
    public partial class newPropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "SubTickets",
                newName: "SendDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MessageOrder",
                table: "SubTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SubTickets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SubTickets_UserId",
                table: "SubTickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTickets_AspNetUsers_UserId",
                table: "SubTickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTickets_AspNetUsers_UserId",
                table: "SubTickets");

            migrationBuilder.DropIndex(
                name: "IX_SubTickets_UserId",
                table: "SubTickets");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MessageOrder",
                table: "SubTickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SubTickets");

            migrationBuilder.RenameColumn(
                name: "SendDateTime",
                table: "SubTickets",
                newName: "SendDate");
        }
    }
}
