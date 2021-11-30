using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_university",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_university", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_education",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gpa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_education", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_tb_m_education_tb_m_university_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_m_university",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_profiling_tb_m_account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_profiling_tb_m_education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_m_education",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_education_UniversityId",
                table: "tb_m_education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_profiling_EducationId",
                table: "tb_t_profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_profiling");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_education");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_university");
        }
    }
}
