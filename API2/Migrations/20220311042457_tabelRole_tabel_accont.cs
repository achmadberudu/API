using Microsoft.EntityFrameworkCore.Migrations;

namespace API2.Migrations
{
    public partial class tabelRole_tabel_accont : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "AccountRole");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AccountRole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AccountRole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "AccountRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
