using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    public partial class Trade_table_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommoditiesIdentifier",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "CounterpartiesIdentifier",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "LocationIdentifier",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "Side",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "TradeDate",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "TradeStatus",
                table: "TradeNotifications");

            migrationBuilder.RenameColumn(
                name: "NotificaitonProccessed",
                table: "TradeNotifications",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "TradeNotifications",
                newName: "NotificaitonProccessed");

            migrationBuilder.AddColumn<string>(
                name: "CommoditiesIdentifier",
                table: "TradeNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CounterpartiesIdentifier",
                table: "TradeNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationIdentifier",
                table: "TradeNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TradeNotifications",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TradeNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "TradeNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TradeDate",
                table: "TradeNotifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TradeStatus",
                table: "TradeNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
