using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpgWebshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliverySlotTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeFrom",
                table: "DeliverySchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeTo",
                table: "DeliverySchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeFrom",
                table: "DeliverySchedules");

            migrationBuilder.DropColumn(
                name: "TimeTo",
                table: "DeliverySchedules");
        }
    }
}
