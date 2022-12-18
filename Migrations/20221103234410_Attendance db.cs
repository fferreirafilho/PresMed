using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations {
    public partial class Attendancedb : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<int>(
                name: "SchedulingId",
                table: "Attendance",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_SchedulingId",
                table: "Attendance",
                column: "SchedulingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Scheduling_SchedulingId",
                table: "Attendance",
                column: "SchedulingId",
                principalTable: "Scheduling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Scheduling_SchedulingId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_SchedulingId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "SchedulingId",
                table: "Attendance");
        }
    }
}
