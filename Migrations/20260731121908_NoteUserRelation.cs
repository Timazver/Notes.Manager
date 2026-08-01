using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Manager.Migrations
{
    /// <inheritdoc />
    public partial class NoteUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "notes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_notes_UserId",
                table: "notes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_UserId",
                table: "notes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_UserId",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_notes_UserId",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "notes");
        }
    }
}
