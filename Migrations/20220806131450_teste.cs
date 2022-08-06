using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Cpf = table.Column<long>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    District = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Complement = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    User = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Crm = table.Column<string>(nullable: false),
                    Speciality = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");
        }
    }
}
