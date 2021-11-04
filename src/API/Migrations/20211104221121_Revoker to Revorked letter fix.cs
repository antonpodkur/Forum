using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RevokertoRevorkedletterfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "RefreshTokens",
                newName: "IsRevorked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRevorked",
                table: "RefreshTokens",
                newName: "IsRevoked");
        }
    }
}
