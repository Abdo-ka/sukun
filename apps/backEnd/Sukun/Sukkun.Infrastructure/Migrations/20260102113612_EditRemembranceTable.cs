using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRemembranceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDaily",
                table: "Remembrances");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Remembrances");

            migrationBuilder.DropColumn(
                name: "IsDaily",
                table: "RemembranceCategories");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "RemembranceCategories");

            migrationBuilder.RenameColumn(
                name: "TitleAr",
                table: "Tasbihs",
                newName: "TitleEn");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "RemembranceContents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "RemembranceContents");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "Tasbihs",
                newName: "TitleAr");

            migrationBuilder.AddColumn<bool>(
                name: "IsDaily",
                table: "Remembrances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Remembrances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDaily",
                table: "RemembranceCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "RemembranceCategories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
