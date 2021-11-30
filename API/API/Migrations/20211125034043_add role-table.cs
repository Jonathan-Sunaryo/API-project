using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addroletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountRoleId",
                table: "tb_m_account",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tb_t_account_role",
                columns: table => new
                {
                    AccountRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNIK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_account_role", x => x.AccountRoleId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_tb_m_role_tb_t_account_role_AccountRoleId",
                        column: x => x.AccountRoleId,
                        principalTable: "tb_t_account_role",
                        principalColumn: "AccountRoleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_AccountRoleId",
                table: "tb_m_account",
                column: "AccountRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_role_AccountRoleId",
                table: "tb_m_role",
                column: "AccountRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_t_account_role_AccountRoleId",
                table: "tb_m_account",
                column: "AccountRoleId",
                principalTable: "tb_t_account_role",
                principalColumn: "AccountRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_t_account_role_AccountRoleId",
                table: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_t_account_role");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_account_AccountRoleId",
                table: "tb_m_account");

            migrationBuilder.DropColumn(
                name: "AccountRoleId",
                table: "tb_m_account");
        }
    }
}
