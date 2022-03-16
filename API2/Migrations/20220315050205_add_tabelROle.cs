using Microsoft.EntityFrameworkCore.Migrations;

namespace API2.Migrations
{
    public partial class add_tabelROle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_tb_tr_account_AccountNIK",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "AccountRole",
                newName: "AccountRoles");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRoles",
                newName: "IX_AccountRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_AccountNIK",
                table: "AccountRoles",
                newName: "IX_AccountRoles_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_tb_tr_account_AccountNIK",
                table: "AccountRoles",
                column: "AccountNIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_tb_tr_account_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "AccountRole");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRole",
                newName: "IX_AccountRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_AccountNIK",
                table: "AccountRole",
                newName: "IX_AccountRole_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_tb_tr_account_AccountNIK",
                table: "AccountRole",
                column: "AccountNIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
