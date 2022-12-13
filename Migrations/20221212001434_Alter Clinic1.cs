using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class AlterClinic1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttestedText",
                table: "ClinicOpening",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeText",
                table: "ClinicOpening",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttestedText",
                table: "ClinicOpening");

            migrationBuilder.DropColumn(
                name: "RecipeText",
                table: "ClinicOpening");
        }
    }
}
