using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class SchedulingChangeAtt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Person_MedicId",
                table: "Scheduling");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Person_PacientId",
                table: "Scheduling");

            migrationBuilder.DropIndex(
                name: "IX_Scheduling_MedicId",
                table: "Scheduling");

            migrationBuilder.DropIndex(
                name: "IX_Scheduling_PacientId",
                table: "Scheduling");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Scheduling");

            migrationBuilder.DropColumn(
                name: "PacientId",
                table: "Scheduling");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Scheduling",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Scheduling",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_DoctorId",
                table: "Scheduling",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_PatientId",
                table: "Scheduling",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Person_DoctorId",
                table: "Scheduling",
                column: "DoctorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Person_PatientId",
                table: "Scheduling",
                column: "PatientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Person_DoctorId",
                table: "Scheduling");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Person_PatientId",
                table: "Scheduling");

            migrationBuilder.DropIndex(
                name: "IX_Scheduling_DoctorId",
                table: "Scheduling");

            migrationBuilder.DropIndex(
                name: "IX_Scheduling_PatientId",
                table: "Scheduling");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Scheduling");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Scheduling");

            migrationBuilder.AddColumn<int>(
                name: "MedicId",
                table: "Scheduling",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacientId",
                table: "Scheduling",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_MedicId",
                table: "Scheduling",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_PacientId",
                table: "Scheduling",
                column: "PacientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Person_MedicId",
                table: "Scheduling",
                column: "MedicId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Person_PacientId",
                table: "Scheduling",
                column: "PacientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
