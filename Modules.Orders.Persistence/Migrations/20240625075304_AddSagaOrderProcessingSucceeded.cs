using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSagaOrderProcessingSucceeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SagaData",
                table: "SagaData");

            migrationBuilder.RenameTable(
                name: "SagaData",
                newName: "OrderProcessingSagaData");

            migrationBuilder.AddColumn<bool>(
                name: "OrderProcessingSucceeded",
                table: "OrderProcessingSagaData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProcessingSagaData",
                table: "OrderProcessingSagaData",
                column: "CorrelationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProcessingSagaData",
                table: "OrderProcessingSagaData");

            migrationBuilder.DropColumn(
                name: "OrderProcessingSucceeded",
                table: "OrderProcessingSagaData");

            migrationBuilder.RenameTable(
                name: "OrderProcessingSagaData",
                newName: "SagaData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SagaData",
                table: "SagaData",
                column: "CorrelationId");
        }
    }
}
