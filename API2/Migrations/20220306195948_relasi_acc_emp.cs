using Microsoft.EntityFrameworkCore.Migrations;

namespace API2.Migrations
{
    public partial class relasi_acc_emp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_university",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_university", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_education_tb_m_university_University_id",
                        column: x => x.University_id,
                        principalTable: "tb_m_university",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_tr_profiling_tb_tr_education_Education_id",
                        column: x => x.Education_id,
                        principalTable: "tb_tr_education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_tb_tr_profiling_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_tr_profiling",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_education_University_id",
                table: "tb_tr_education",
                column: "University_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_profiling_Education_id",
                table: "tb_tr_profiling",
                column: "Education_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_account");

            migrationBuilder.DropTable(
                name: "tb_tr_profiling");

            migrationBuilder.DropTable(
                name: "tb_tr_education");

            migrationBuilder.DropTable(
                name: "tb_m_university");
        }
    }
}
