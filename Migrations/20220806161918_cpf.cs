using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class cpf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Cpf",
                table: "Doctor",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User",
                table: "Doctor",
                column: "User",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctor_Cpf",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_User",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Doctor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "Cpf",
                table: "Doctor",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
