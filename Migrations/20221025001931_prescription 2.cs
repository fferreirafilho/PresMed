using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class prescription2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendanceId",
                table: "Medicine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_AttendanceId",
                table: "Medicine",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Attendance_AttendanceId",
                table: "Medicine",
                column: "AttendanceId",
                principalTable: "Attendance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Attendance_AttendanceId",
                table: "Medicine");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_AttendanceId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "Medicine");
        }
    }
}
