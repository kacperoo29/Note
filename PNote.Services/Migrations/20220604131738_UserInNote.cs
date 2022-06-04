using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PNote.Services.Migrations
{
    public partial class UserInNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_NoteUserId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "NoteUserId",
                table: "Notes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_NoteUserId",
                table: "Notes",
                newName: "IX_Notes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notes",
                newName: "NoteUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                newName: "IX_Notes_NoteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_NoteUserId",
                table: "Notes",
                column: "NoteUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
