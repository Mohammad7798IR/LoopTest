using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Host.Migrations
{
    public partial class FixAddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NamtionalCode",
                table: "User",
                newName: "NationalCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalCode",
                table: "User",
                newName: "NamtionalCode");
        }
    }
}
