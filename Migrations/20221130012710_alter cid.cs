using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class altercid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cod",
                table: "Cid",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3) CHARACTER SET utf8mb4",
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cod",
                table: "Cid",
                type: "varchar(3) CHARACTER SET utf8mb4",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }
    }
}
