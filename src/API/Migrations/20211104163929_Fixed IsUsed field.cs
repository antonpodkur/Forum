using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class FixedIsUsedfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUser",
                table: "RefreshTokens",
                newName: "IsUsed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "RefreshTokens",
                newName: "IsUser");
        }
    }
}
