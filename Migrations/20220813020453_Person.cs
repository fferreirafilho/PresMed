using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations {
    public partial class Person : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Persons");

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

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    City = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    Complement = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    Cpf = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Crm = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    District = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "varchar(7) CHARACTER SET utf8mb4", maxLength: 7, nullable: true),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Speciality = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    User = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    City = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    Complement = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    Cpf = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Crm = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    District = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "varchar(7) CHARACTER SET utf8mb4", maxLength: 7, nullable: true),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Speciality = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    State = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false),
                    User = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Cpf",
                table: "Doctor",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Crm",
                table: "Doctor",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User",
                table: "Doctor",
                column: "User",
                unique: true);

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
    }
}
