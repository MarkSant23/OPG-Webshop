using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpgWebshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PublicId",
                table: "Products",
                column: "PublicId",
                unique: true,
                filter: "[PublicId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_PublicId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Products");
        }
    }
}
