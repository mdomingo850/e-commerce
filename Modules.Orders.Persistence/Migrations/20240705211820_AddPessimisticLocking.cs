﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPessimisticLocking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "OutboxMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "OutboxMessages");
        }
    }
}
