using Microsoft.EntityFrameworkCore.Migrations;

namespace Reminder.Infrastructure.DataAccess.Migrations
{
    public partial class DeleteNotificationCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Todos_TodoId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Todos_TodoId",
                table: "Notifications",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Todos_TodoId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Todos_TodoId",
                table: "Notifications",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
