using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "IslamicBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookContents_IslamicBookSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "IslamicBookSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookContents_DisplayOrder",
                table: "BookContents",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_BookContents_SectionId",
                table: "BookContents",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookContents");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "IslamicBooks");
        }
    }
}
