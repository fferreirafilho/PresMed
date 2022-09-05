using Microsoft.EntityFrameworkCore.Migrations;

namespace PresMed.Migrations
{
    public partial class ProceduresSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProceduresId",
                table: "Scheduling",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_ProceduresId",
                table: "Scheduling",
                column: "ProceduresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Procedure_ProceduresId",
                table: "Scheduling",
                column: "ProceduresId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Procedure_ProceduresId",
                table: "Scheduling");

            migrationBuilder.DropIndex(
                name: "IX_Scheduling_ProceduresId",
                table: "Scheduling");

            migrationBuilder.DropColumn(
                name: "ProceduresId",
                table: "Scheduling");
        }
    }
}
