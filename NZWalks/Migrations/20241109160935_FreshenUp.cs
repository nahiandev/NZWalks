using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class FreshenUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("73b191f8-fe5f-4a0e-b7d3-c04a886f1c46"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b123f4de-5678-90ab-cdef-111213141516"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c67890ab-1234-56de-7890-123456789012"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d043934d-3481-48df-860a-e8199e9aa923"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d7890123-4567-89ab-cdef-098765432123"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e0123456-7890-12ab-cdef-123456789abc"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("73b191f8-fe5f-4a0e-b7d3-c04a886f1c46"), "LAS", "Washington", "https://dummyimage.com/400x400/ffff00/000.png" },
                    { new Guid("b123f4de-5678-90ab-cdef-111213141516"), "CAF", "California", "https://dummyimage.com/400x400/ff0000/fff.png" },
                    { new Guid("c67890ab-1234-56de-7890-123456789012"), "NYK", "New York", "https://dummyimage.com/400x400/00ff00/fff.png" },
                    { new Guid("d043934d-3481-48df-860a-e8199e9aa923"), "FL", "Florida", "https://dummyimage.com/400x400/000/fff.png" },
                    { new Guid("d7890123-4567-89ab-cdef-098765432123"), "TXS", "Texas", "https://dummyimage.com/400x400/0000ff/fff.png" },
                    { new Guid("e0123456-7890-12ab-cdef-123456789abc"), "WAS", "Washington", "https://dummyimage.com/400x400/ffff00/000.png" }
                });
        }
    }
}
