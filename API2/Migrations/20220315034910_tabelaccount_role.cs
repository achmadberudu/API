using Microsoft.EntityFrameworkCore.Migrations;

namespace API2.Migrations
{
    public partial class tabelaccount_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIK",
                table: "AccountRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NIK",
                table: "AccountRole",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
