using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class Atestado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalCertificateId",
                table: "Cid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicalCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttendanceId = table.Column<int>(nullable: true),
                    CidId = table.Column<int>(nullable: false),
                    Days = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalCertificates_Attendance_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalCertificates_Cid_CidId",
                        column: x => x.CidId,
                        principalTable: "Cid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cid_MedicalCertificateId",
                table: "Cid",
                column: "MedicalCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificates_AttendanceId",
                table: "MedicalCertificates",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificates_CidId",
                table: "MedicalCertificates",
                column: "CidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cid_MedicalCertificates_MedicalCertificateId",
                table: "Cid",
                column: "MedicalCertificateId",
                principalTable: "MedicalCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cid_MedicalCertificates_MedicalCertificateId",
                table: "Cid");

            migrationBuilder.DropTable(
                name: "MedicalCertificates");

            migrationBuilder.DropIndex(
                name: "IX_Cid_MedicalCertificateId",
                table: "Cid");

            migrationBuilder.DropColumn(
                name: "MedicalCertificateId",
                table: "Cid");
        }
    }
}
