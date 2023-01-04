using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    public partial class notificaitonaddnotificationproccessedinTradeNoticationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_TradeNotifications_TradeNotificationId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_TradeNotificationId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SMSRetries",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "TradeNotificationId",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "NotificaitonProccess",
                table: "TradeNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificaitonProccess",
                table: "TradeNotifications");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "SMSRetries",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TradeNotificationId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TradeNotificationId",
                table: "Notifications",
                column: "TradeNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_TradeNotifications_TradeNotificationId",
                table: "Notifications",
                column: "TradeNotificationId",
                principalTable: "TradeNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
