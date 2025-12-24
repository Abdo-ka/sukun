using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTasbihAndMosque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RemembranceCategoryLinks_RemembranceCategories_CategoriesId",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RemembranceCategoryLinks_Remembrances_RemembrancesId",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RemembranceCategoryLinks",
                table: "RemembranceCategoryLinks");

            migrationBuilder.RenameColumn(
                name: "RemembrancesId",
                table: "RemembranceCategoryLinks",
                newName: "RemembranceId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "RemembranceCategoryLinks",
                newName: "RemembranceCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RemembranceCategoryLinks_RemembrancesId",
                table: "RemembranceCategoryLinks",
                newName: "IX_RemembranceCategoryLinks_RemembranceId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RemembranceCategoryLinks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "RemembranceCategoryLinks",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RemembranceCategoryLinks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RemembranceCategoryLinks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemembranceCategoryLinks",
                table: "RemembranceCategoryLinks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Mosques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HasPrayerFacilities = table.Column<bool>(type: "bit", nullable: false),
                    HasWomenSection = table.Column<bool>(type: "bit", nullable: false),
                    HasParking = table.Column<bool>(type: "bit", nullable: false),
                    IsJummahMasjid = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mosques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mosques_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasbihs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RecommendedCount = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasbihs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemembranceCategoryLinks_RemembranceCategoryId",
                table: "RemembranceCategoryLinks",
                column: "RemembranceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Mosques_CityId",
                table: "Mosques",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RemembranceCategoryLinks_RemembranceCategories_RemembranceCategoryId",
                table: "RemembranceCategoryLinks",
                column: "RemembranceCategoryId",
                principalTable: "RemembranceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RemembranceCategoryLinks_Remembrances_RemembranceId",
                table: "RemembranceCategoryLinks",
                column: "RemembranceId",
                principalTable: "Remembrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RemembranceCategoryLinks_RemembranceCategories_RemembranceCategoryId",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RemembranceCategoryLinks_Remembrances_RemembranceId",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropTable(
                name: "Mosques");

            migrationBuilder.DropTable(
                name: "Tasbihs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RemembranceCategoryLinks",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropIndex(
                name: "IX_RemembranceCategoryLinks_RemembranceCategoryId",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RemembranceCategoryLinks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RemembranceCategoryLinks");

            migrationBuilder.RenameColumn(
                name: "RemembranceId",
                table: "RemembranceCategoryLinks",
                newName: "RemembrancesId");

            migrationBuilder.RenameColumn(
                name: "RemembranceCategoryId",
                table: "RemembranceCategoryLinks",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_RemembranceCategoryLinks_RemembranceId",
                table: "RemembranceCategoryLinks",
                newName: "IX_RemembranceCategoryLinks_RemembrancesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemembranceCategoryLinks",
                table: "RemembranceCategoryLinks",
                columns: new[] { "CategoriesId", "RemembrancesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RemembranceCategoryLinks_RemembranceCategories_CategoriesId",
                table: "RemembranceCategoryLinks",
                column: "CategoriesId",
                principalTable: "RemembranceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RemembranceCategoryLinks_Remembrances_RemembrancesId",
                table: "RemembranceCategoryLinks",
                column: "RemembrancesId",
                principalTable: "Remembrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
