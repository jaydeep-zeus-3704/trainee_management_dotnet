using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainee_management.Migrations
{
    /// <inheritdoc />
    public partial class SubmissionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TraineeId",
                table: "Submission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Submission_TraineeId",
                table: "Submission",
                column: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Trainee_TraineeId",
                table: "Submission",
                column: "TraineeId",
                principalTable: "Trainee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Trainee_TraineeId",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Submission_TraineeId",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "TraineeId",
                table: "Submission");
        }
    }
}
