using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class prescription3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceMedicines_Attendance_AttendanceId",
                table: "AttendanceMedicines");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceMedicines_Medicine_Medicineid",
                table: "AttendanceMedicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceMedicines",
                table: "AttendanceMedicines");

            migrationBuilder.RenameTable(
                name: "AttendanceMedicines",
                newName: "Prescription");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceMedicines_Medicineid",
                table: "Prescription",
                newName: "IX_Prescription_Medicineid");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceMedicines_AttendanceId",
                table: "Prescription",
                newName: "IX_Prescription_AttendanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Attendance_AttendanceId",
                table: "Prescription",
                column: "AttendanceId",
                principalTable: "Attendance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicine_Medicineid",
                table: "Prescription",
                column: "Medicineid",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Attendance_AttendanceId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicine_Medicineid",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "AttendanceMedicines");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_Medicineid",
                table: "AttendanceMedicines",
                newName: "IX_AttendanceMedicines_Medicineid");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_AttendanceId",
                table: "AttendanceMedicines",
                newName: "IX_AttendanceMedicines_AttendanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceMedicines",
                table: "AttendanceMedicines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceMedicines_Attendance_AttendanceId",
                table: "AttendanceMedicines",
                column: "AttendanceId",
                principalTable: "Attendance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceMedicines_Medicine_Medicineid",
                table: "AttendanceMedicines",
                column: "Medicineid",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
