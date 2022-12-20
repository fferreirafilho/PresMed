using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class Clinica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicOpening");

            migrationBuilder.CreateTable(
                name: "ClinicSetings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InitialHour = table.Column<DateTime>(nullable: false),
                    EndHour = table.Column<DateTime>(nullable: false),
                    Street = table.Column<string>(maxLength: 20, nullable: false),
                    District = table.Column<string>(maxLength: 40, nullable: false),
                    State = table.Column<string>(maxLength: 20, nullable: false),
                    Complement = table.Column<string>(maxLength: 40, nullable: true),
                    City = table.Column<string>(maxLength: 20, nullable: false),
                    Number = table.Column<string>(maxLength: 7, nullable: false),
                    RecipeText = table.Column<string>(maxLength: 500, nullable: false),
                    AttestedText = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicSetings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicSetings");

            migrationBuilder.CreateTable(
                name: "ClinicOpening",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttestedText = table.Column<string>(type: "varchar(500) CHARACTER SET utf8mb4", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    Complement = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    District = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: false),
                    EndHour = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InitialHour = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Number = table.Column<string>(type: "varchar(7) CHARACTER SET utf8mb4", maxLength: 7, nullable: false),
                    RecipeText = table.Column<string>(type: "varchar(500) CHARACTER SET utf8mb4", maxLength: 500, nullable: false),
                    State = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    Street = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicOpening", x => x.Id);
                });
        }
    }
}
