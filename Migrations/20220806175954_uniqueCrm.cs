using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class uniqueCrm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Crm",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Crm",
                table: "Doctor",
                column: "Crm",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctor_Crm",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "Crm",
                table: "Doctor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
