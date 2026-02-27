using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpgWebshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderNumberSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderNumbers",
                startValue: 10001L);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR OrderNumbers");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");

            migrationBuilder.DropSequence(
                name: "OrderNumbers");
        }
    }
}
