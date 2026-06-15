using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainee_management.Migrations
{
    /// <inheritdoc />
    public partial class submission3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Submission_TaskAssignmentId",
                table: "Submission",
                column: "TaskAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_TaskAssignment_TaskAssignmentId",
                table: "Submission",
                column: "TaskAssignmentId",
                principalTable: "TaskAssignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_TaskAssignment_TaskAssignmentId",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Submission_TaskAssignmentId",
                table: "Submission");
        }
    }
}
