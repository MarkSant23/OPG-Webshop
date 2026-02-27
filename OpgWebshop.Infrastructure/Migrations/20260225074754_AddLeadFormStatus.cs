using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpgWebshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadFormStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LeadForms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeadForms");
        }
    }
}
