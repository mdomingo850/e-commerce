using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Inventories.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLockingForInboxMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "InboxMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "InboxMessage",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "InboxMessage");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "InboxMessage");
        }
    }
}
