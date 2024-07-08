using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOptimisticLocking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "OutboxMessages",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "OutboxMessages");
        }
    }
}
