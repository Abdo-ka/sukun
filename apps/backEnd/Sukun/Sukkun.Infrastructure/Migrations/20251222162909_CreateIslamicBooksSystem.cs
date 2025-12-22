using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateIslamicBooksSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Hadiths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "Hadiths",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IslamicBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslamicBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IslamicBookSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslamicBookSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IslamicBookSections_IslamicBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "IslamicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_BookId",
                table: "Hadiths",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_SectionId",
                table: "Hadiths",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_IslamicBookSections_BookId",
                table: "IslamicBookSections",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hadiths_IslamicBookSections_SectionId",
                table: "Hadiths",
                column: "SectionId",
                principalTable: "IslamicBookSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Hadiths_IslamicBooks_BookId",
                table: "Hadiths",
                column: "BookId",
                principalTable: "IslamicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hadiths_IslamicBookSections_SectionId",
                table: "Hadiths");

            migrationBuilder.DropForeignKey(
                name: "FK_Hadiths_IslamicBooks_BookId",
                table: "Hadiths");

            migrationBuilder.DropTable(
                name: "IslamicBookSections");

            migrationBuilder.DropTable(
                name: "IslamicBooks");

            migrationBuilder.DropIndex(
                name: "IX_Hadiths_BookId",
                table: "Hadiths");

            migrationBuilder.DropIndex(
                name: "IX_Hadiths_SectionId",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Hadiths");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Hadiths");
        }
    }
}
