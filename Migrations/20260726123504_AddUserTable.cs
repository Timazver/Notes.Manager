using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Manager.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_notes",
                table: "notes");

            migrationBuilder.RenameTable(
                name: "notes",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Notes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Notes",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Notes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Notes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Notes",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Notes",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "notes",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "notes",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "notes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "notes",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "notes",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "notes",
                newName: "created_at");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notes",
                table: "notes",
                column: "id");
        }
    }
}
