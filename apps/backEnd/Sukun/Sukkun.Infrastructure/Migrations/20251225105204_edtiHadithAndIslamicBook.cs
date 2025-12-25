using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class edtiHadithAndIslamicBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hadiths_Grade",
                table: "Hadiths");

            migrationBuilder.DropIndex(
                name: "IX_Hadiths_Reference",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "IslamicBookSections");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "IslamicBooks");

            migrationBuilder.DropColumn(
                name: "BookName",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "ChapterName",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "GradeExplanation",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "GradedBy",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "Translation",
                table: "Hadiths");

            migrationBuilder.RenameColumn(
                name: "Transliteration",
                table: "Hadiths",
                newName: "HeadingArabic");

            migrationBuilder.AddColumn<int>(
                name: "ChapterNumber",
                table: "IslamicBookSections",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Hadiths",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Hadiths",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "Hadiths",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterNumber",
                table: "IslamicBookSections");

            migrationBuilder.RenameColumn(
                name: "HeadingArabic",
                table: "Hadiths",
                newName: "Transliteration");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "IslamicBookSections",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "IslamicBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Hadiths",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Hadiths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "Hadiths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "Hadiths",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChapterName",
                table: "Hadiths",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeExplanation",
                table: "Hadiths",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradedBy",
                table: "Hadiths",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Translation",
                table: "Hadiths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_Grade",
                table: "Hadiths",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_Reference",
                table: "Hadiths",
                column: "Reference");
        }
    }
}
