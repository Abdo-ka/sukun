using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataVersioning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Tasbihs",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Tafsirs",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Remembrances",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "RemembranceContents",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "RemembranceCategoryLinks",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "RemembranceCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "QuranVerses",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "QuranSurahs",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "NarrativeSections",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Narratives",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Mosques",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "IslamicBookSections",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "IslamicBooks",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Hadiths",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "HadithExplanations",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "HadithCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "DuaItems",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "DuaCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Cities",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "BookContents",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "BaseEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "AsmaulHusna",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Admins",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateTable(
                name: "DataVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataVersions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataVersions_TableName",
                table: "DataVersions",
                column: "TableName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataVersions");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Tasbihs");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Tafsirs");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Remembrances");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RemembranceContents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RemembranceCategories");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "QuranVerses");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "QuranSurahs");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "NarrativeSections");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Narratives");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Mosques");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "IslamicBookSections");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "IslamicBooks");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "HadithExplanations");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "HadithCategories");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "DuaItems");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "DuaCategories");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "BookContents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "AsmaulHusna");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Admins");
        }
    }
}
