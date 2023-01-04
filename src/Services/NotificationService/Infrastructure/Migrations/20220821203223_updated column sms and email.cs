using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    public partial class updatedcolumnsmsandemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSent",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SMSSent",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Notifications",
                newName: "SMSStatus");

            migrationBuilder.AddColumn<int>(
                name: "EmailStatus",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailStatus",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "SMSStatus",
                table: "Notifications",
                newName: "Status");

            migrationBuilder.AddColumn<bool>(
                name: "EmailSent",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SMSSent",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
