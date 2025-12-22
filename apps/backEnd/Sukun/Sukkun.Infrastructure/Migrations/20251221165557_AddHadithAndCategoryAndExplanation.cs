using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHadithAndCategoryAndExplanation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HadithCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HadithCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hadiths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transliteration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    GradedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GradeExplanation = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BookName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChapterName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    HadithNumber = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hadiths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hadiths_HadithCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "HadithCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HadithExplanations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    HadithId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Scholar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HadithExplanations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HadithExplanations_Hadiths_HadithId",
                        column: x => x.HadithId,
                        principalTable: "Hadiths",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HadithExplanations_HadithId",
                table: "HadithExplanations",
                column: "HadithId");

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_CategoryId",
                table: "Hadiths",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_Grade",
                table: "Hadiths",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_Reference",
                table: "Hadiths",
                column: "Reference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HadithExplanations");

            migrationBuilder.DropTable(
                name: "Hadiths");

            migrationBuilder.DropTable(
                name: "HadithCategories");
        }
    }
}
