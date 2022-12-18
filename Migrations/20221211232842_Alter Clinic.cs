using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class AlterClinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ClinicOpening",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "ClinicOpening",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "ClinicOpening",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "ClinicOpening",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ClinicOpening",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "ClinicOpening",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ClinicOpening");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "ClinicOpening");

            migrationBuilder.DropColumn(
                name: "District",
                table: "ClinicOpening");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "ClinicOpening");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ClinicOpening");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "ClinicOpening");
        }
    }
}
