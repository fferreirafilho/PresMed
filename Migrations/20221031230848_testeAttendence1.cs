using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class testeAttendence1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Attendance",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Attendance",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_DoctorId",
                table: "Attendance",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_PatientId",
                table: "Attendance",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Person_DoctorId",
                table: "Attendance",
                column: "DoctorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Person_PatientId",
                table: "Attendance",
                column: "PatientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Person_DoctorId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Person_PatientId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_DoctorId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_PatientId",
                table: "Attendance");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Attendance",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Attendance",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
