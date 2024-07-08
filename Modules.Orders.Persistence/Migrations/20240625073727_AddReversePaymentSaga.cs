using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReversePaymentSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OrderPaymentReversed",
                table: "SagaData",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPaymentReversed",
                table: "SagaData");
        }
    }
}
