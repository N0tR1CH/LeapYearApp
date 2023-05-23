using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeapYearApp.Migrations
{
    public partial class AddLoginToYearNameForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "YearNameForms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "YearNameForms");
        }
    }
}
