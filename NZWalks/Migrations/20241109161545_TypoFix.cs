using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class TypoFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImamgeUrl",
                table: "Walks",
                newName: "WalkImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImageUrl",
                table: "Walks",
                newName: "WalkImamgeUrl");
        }
    }
}
