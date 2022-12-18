using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class Atestado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cid_MedicalCertificates_MedicalCertificateId",
                table: "Cid");

            migrationBuilder.DropIndex(
                name: "IX_Cid_MedicalCertificateId",
                table: "Cid");

            migrationBuilder.DropColumn(
                name: "MedicalCertificateId",
                table: "Cid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalCertificateId",
                table: "Cid",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cid_MedicalCertificateId",
                table: "Cid",
                column: "MedicalCertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cid_MedicalCertificates_MedicalCertificateId",
                table: "Cid",
                column: "MedicalCertificateId",
                principalTable: "MedicalCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
