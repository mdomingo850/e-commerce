using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Customers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { new Guid("ac8572ba-8742-43be-ac63-fd69654a7188"), "Clark", "Kent" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("ac8572ba-8742-43be-ac63-fd69654a7188"));
        }
    }
}
