using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecipeText",
                table: "ClinicOpening",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "ClinicOpening",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7) CHARACTER SET utf8mb4",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttestedText",
                table: "ClinicOpening",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecipeText",
                table: "ClinicOpening",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "ClinicOpening",
                type: "varchar(7) CHARACTER SET utf8mb4",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "AttestedText",
                table: "ClinicOpening",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);
        }
    }
}
