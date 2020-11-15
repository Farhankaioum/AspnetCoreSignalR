using Microsoft.EntityFrameworkCore.Migrations;

namespace RawCoding_ChapAPp.Data.Migrations
{
    public partial class NameColumnAddedInChatEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chats");
        }
    }
}
