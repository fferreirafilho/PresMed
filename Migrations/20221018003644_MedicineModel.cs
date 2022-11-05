using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class MedicineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicine_Name",
                table: "Medicine");

            migrationBuilder.AddColumn<string>(
                name: "Concentration",
                table: "Medicine",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Drug",
                table: "Medicine",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PharmaceuticalForm",
                table: "Medicine",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Record",
                table: "Medicine",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concentration",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "Drug",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "PharmaceuticalForm",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "Record",
                table: "Medicine");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Name",
                table: "Medicine",
                column: "Name",
                unique: true);
        }
    }
}
