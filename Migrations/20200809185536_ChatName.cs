using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRChatApp.Migrations
{
    public partial class ChatName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Chats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Chats");
        }
    }
}
