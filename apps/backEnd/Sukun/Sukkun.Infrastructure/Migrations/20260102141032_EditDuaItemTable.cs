using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditDuaItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicText",
                table: "DuaItems");

            migrationBuilder.DropColumn(
                name: "RepeatCount",
                table: "DuaItems");

            migrationBuilder.DropColumn(
                name: "Translation",
                table: "DuaItems");

            migrationBuilder.RenameColumn(
                name: "Transliteration",
                table: "DuaItems",
                newName: "TextEn");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "DuaItems",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "DuaItems");

            migrationBuilder.RenameColumn(
                name: "TextEn",
                table: "DuaItems",
                newName: "Transliteration");

            migrationBuilder.AddColumn<string>(
                name: "ArabicText",
                table: "DuaItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RepeatCount",
                table: "DuaItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Translation",
                table: "DuaItems",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
