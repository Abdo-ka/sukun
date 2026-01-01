using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNarrativeTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativesId",
                table: "NarrativeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Tags_TagsId",
                table: "NarrativeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NarrativeTags",
                table: "NarrativeTags");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "NarrativeTags",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "NarrativesId",
                table: "NarrativeTags",
                newName: "NarrativeId");

            migrationBuilder.RenameIndex(
                name: "IX_NarrativeTags_TagsId",
                table: "NarrativeTags",
                newName: "IX_NarrativeTags_TagId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "NarrativeTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "NarrativeTags",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NarrativeTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "NarrativeTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "NarrativeTags",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NarrativeTags",
                table: "NarrativeTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NarrativeTags_NarrativeId",
                table: "NarrativeTags",
                column: "NarrativeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativeId",
                table: "NarrativeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NarrativeTags_Tags_TagId",
                table: "NarrativeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NarrativeTags",
                table: "NarrativeTags");

            migrationBuilder.DropIndex(
                name: "IX_NarrativeTags_NarrativeId",
                table: "NarrativeTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NarrativeTags");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "NarrativeTags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NarrativeTags");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "NarrativeTags");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "NarrativeTags");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "NarrativeTags",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "NarrativeId",
                table: "NarrativeTags",
                newName: "NarrativesId");

            migrationBuilder.RenameIndex(
                name: "IX_NarrativeTags_TagId",
                table: "NarrativeTags",
                newName: "IX_NarrativeTags_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NarrativeTags",
                table: "NarrativeTags",
                columns: new[] { "NarrativesId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeTags_Narratives_NarrativesId",
                table: "NarrativeTags",
                column: "NarrativesId",
                principalTable: "Narratives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NarrativeTags_Tags_TagsId",
                table: "NarrativeTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
