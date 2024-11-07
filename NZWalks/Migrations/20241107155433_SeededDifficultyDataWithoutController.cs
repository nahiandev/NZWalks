﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class SeededDifficultyDataWithoutController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("b123f4de-5678-90ab-cdef-111213141516"), "CA", "California", "https://dummyimage.com/400x400/ff0000/fff.png" },
                    { new Guid("c67890ab-1234-56de-7890-123456789012"), "NY", "New York", "https://dummyimage.com/400x400/00ff00/fff.png" },
                    { new Guid("d043934d-3481-48df-860a-e8199e9aa923"), "FL", "Florida", "https://dummyimage.com/400x400/000/fff.png" },
                    { new Guid("d7890123-4567-89ab-cdef-098765432123"), "TX", "Texas", "https://dummyimage.com/400x400/0000ff/fff.png" },
                    { new Guid("e0123456-7890-12ab-cdef-123456789abc"), "WA", "Washington", "https://dummyimage.com/400x400/ffff00/000.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
