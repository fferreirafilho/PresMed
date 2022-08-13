using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class Persons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Cpf = table.Column<string>(nullable: false),
                    Street = table.Column<string>(maxLength: 20, nullable: false),
                    District = table.Column<string>(maxLength: 40, nullable: false),
                    State = table.Column<string>(maxLength: 20, nullable: false),
                    Complement = table.Column<string>(maxLength: 40, nullable: true),
                    City = table.Column<string>(maxLength: 20, nullable: false),
                    Number = table.Column<string>(maxLength: 7, nullable: true),
                    User = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    PersonType = table.Column<int>(nullable: false),
                    Crm = table.Column<string>(nullable: true),
                    Speciality = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Cpf",
                table: "Persons",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Crm",
                table: "Persons",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_User",
                table: "Persons",
                column: "User",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
