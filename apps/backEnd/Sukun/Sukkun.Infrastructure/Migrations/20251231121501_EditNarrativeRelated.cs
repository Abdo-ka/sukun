using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditNarrativeRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HadithExplanations_Hadiths_HadithId",
                table: "HadithExplanations");

            migrationBuilder.DropForeignKey(
                name: "FK_Narratives_Narratives_ParentId",
                table: "Narratives");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeSections_Narratives_NarrativeId",
                table: "NarrativeSections");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativeId",
                table: "NarrativeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Tags_TagId",
                table: "NarrativeTags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagType",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Narratives_IsMainSection",
                table: "Narratives");

            migrationBuilder.DropIndex(
                name: "IX_Narratives_ParentId",
                table: "Narratives");

            migrationBuilder.DropColumn(
                name: "TagType",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsMainSection",
                table: "Narratives");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Narratives");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Tags",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "NarrativeSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ViewCount",
                table: "Narratives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFeatured",
                table: "Narratives",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMainSection = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NarrativeCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NarrativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarrativeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NarrativeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NarrativeCategories_Narratives_NarrativeId",
                        column: x => x.NarrativeId,
                        principalTable: "Narratives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsMainSection",
                table: "Categories",
                column: "IsMainSection");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Order",
                table: "Categories",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NarrativeCategories_CategoryId",
                table: "NarrativeCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NarrativeCategories_NarrativeId",
                table: "NarrativeCategories",
                column: "NarrativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HadithExplanations_Hadiths_HadithId",
                table: "HadithExplanations",
                column: "HadithId",
                principalTable: "Hadiths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeSections_Narratives_NarrativeId",
                table: "NarrativeSections",
                column: "NarrativeId",
                principalTable: "Narratives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativeId",
                table: "NarrativeTags",
                column: "NarrativeId",
                principalTable: "Narratives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeTags_Tags_TagId",
                table: "NarrativeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HadithExplanations_Hadiths_HadithId",
                table: "HadithExplanations");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeSections_Narratives_NarrativeId",
                table: "NarrativeSections");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativeId",
                table: "NarrativeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Tags_TagId",
                table: "NarrativeTags");

            migrationBuilder.DropTable(
                name: "NarrativeCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "NameEn");

            migrationBuilder.AddColumn<int>(
                name: "TagType",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "NarrativeSections",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ViewCount",
                table: "Narratives",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFeatured",
                table: "Narratives",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainSection",
                table: "Narratives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Narratives",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagType",
                table: "Tags",
                column: "TagType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narratives_IsMainSection",
                table: "Narratives",
                column: "IsMainSection");

            migrationBuilder.CreateIndex(
                name: "IX_Narratives_ParentId",
                table: "Narratives",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HadithExplanations_Hadiths_HadithId",
                table: "HadithExplanations",
                column: "HadithId",
                principalTable: "Hadiths",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Narratives_Narratives_ParentId",
                table: "Narratives",
                column: "ParentId",
                principalTable: "Narratives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeSections_Narratives_NarrativeId",
                table: "NarrativeSections",
                column: "NarrativeId",
                principalTable: "Narratives",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativeId",
                table: "NarrativeTags",
                column: "NarrativeId",
                principalTable: "Narratives",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeTags_Tags_TagId",
                table: "NarrativeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
