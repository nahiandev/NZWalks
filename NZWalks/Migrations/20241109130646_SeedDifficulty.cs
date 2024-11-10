using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class SeedDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a351cdcd-178f-406d-bfe5-5a58d9ae2349"), "Medium" },
                    { new Guid("cbd6405b-5b60-4de4-a2de-eb55a410d1bf"), "Easy" },
                    { new Guid("d9d1a2e3-92c1-45cc-8cf3-a307a0ec6984"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a351cdcd-178f-406d-bfe5-5a58d9ae2349"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cbd6405b-5b60-4de4-a2de-eb55a410d1bf"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d9d1a2e3-92c1-45cc-8cf3-a307a0ec6984"));
        }
    }
}
