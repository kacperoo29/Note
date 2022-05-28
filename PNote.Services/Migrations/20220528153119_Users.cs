using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PNote.Services.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Notes");

            migrationBuilder.AddColumn<Guid>(
                name: "NoteUserId",
                table: "Notes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PinnedNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    X = table.Column<double>(type: "REAL", nullable: false),
                    Y = table.Column<double>(type: "REAL", nullable: false),
                    NoteId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinnedNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinnedNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteUserId",
                table: "Notes",
                column: "NoteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedNotes_NoteId",
                table: "PinnedNotes",
                column: "NoteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_NoteUserId",
                table: "Notes",
                column: "NoteUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_NoteUserId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "PinnedNotes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteUserId",
                table: "Notes");

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
