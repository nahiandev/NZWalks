using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDifficultyWithoutController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1c900f7f-82be-4ea7-8908-1a748fc5bcae"), "Hard" },
                    { new Guid("6c1366bd-7194-4450-b22b-63ad8420d137"), "Medium" },
                    { new Guid("d043934d-3481-48df-860a-e8199e9aa923"), "Easy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1c900f7f-82be-4ea7-8908-1a748fc5bcae"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6c1366bd-7194-4450-b22b-63ad8420d137"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d043934d-3481-48df-860a-e8199e9aa923"));
        }
    }
}
