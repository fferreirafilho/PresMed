using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class Atestado4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cid",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150) CHARACTER SET utf8mb4",
                oldMaxLength: 150);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cid",
                type: "varchar(150) CHARACTER SET utf8mb4",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
