using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Inventories.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseSeedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("5f341ec0-38f2-4a3e-84d7-1eb51885a95d"),
                column: "Quantity",
                value: 1000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("5f341ec0-38f2-4a3e-84d7-1eb51885a95d"),
                column: "Quantity",
                value: 100);
        }
    }
}
