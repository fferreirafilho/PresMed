using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations {
    public partial class Teste : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new {
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
                    Crm = table.Column<string>(maxLength: 20, nullable: false),
                    Speciality = table.Column<string>(maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PersonType = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_Cpf",
                table: "Person",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Crm",
                table: "Person",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_User",
                table: "Person",
                column: "User",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
