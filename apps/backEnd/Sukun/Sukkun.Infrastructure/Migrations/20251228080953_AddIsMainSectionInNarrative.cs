using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsMainSectionInNarrative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainSection",
                table: "Narratives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Narratives_IsMainSection",
                table: "Narratives",
                column: "IsMainSection");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Narratives_IsMainSection",
                table: "Narratives");

            migrationBuilder.DropColumn(
                name: "IsMainSection",
                table: "Narratives");
        }
    }
}
