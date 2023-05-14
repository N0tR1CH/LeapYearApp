using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeapYearApp.Migrations
{
    public partial class AddResultColumnToYearNameFormTableSecondTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "YearNameForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "YearNameForms");
        }
    }
}
