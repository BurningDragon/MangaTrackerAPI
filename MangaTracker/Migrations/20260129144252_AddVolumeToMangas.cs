using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddVolumeToMangas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Mangas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Mangas");
        }
    }
}
