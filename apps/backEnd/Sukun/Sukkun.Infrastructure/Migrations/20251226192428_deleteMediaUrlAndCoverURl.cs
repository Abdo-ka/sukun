using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteMediaUrlAndCoverURl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "NarrativeSections");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Narratives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Tags",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "NarrativeSections",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Narratives",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
