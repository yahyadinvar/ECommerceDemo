using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakePaymentMethodRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethod",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethod",
                table: "Orders",
                column: "PaymentMethod",
                unique: true);
        }
    }
}
