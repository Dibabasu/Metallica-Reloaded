using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    public partial class updatedcolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificaitonProccess",
                table: "TradeNotifications",
                newName: "NotificaitonProccessed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificaitonProccessed",
                table: "TradeNotifications",
                newName: "NotificaitonProccess");
        }
    }
}
