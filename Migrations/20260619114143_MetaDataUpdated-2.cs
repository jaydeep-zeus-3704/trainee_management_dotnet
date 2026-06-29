using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainee_management.Migrations
{
    /// <inheritdoc />
    public partial class MetaDataUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmissionId",
                table: "Metadata",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_SubmissionId",
                table: "Metadata",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metadata_Submission_SubmissionId",
                table: "Metadata",
                column: "SubmissionId",
                principalTable: "Submission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metadata_Submission_SubmissionId",
                table: "Metadata");

            migrationBuilder.DropIndex(
                name: "IX_Metadata_SubmissionId",
                table: "Metadata");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "Metadata");
        }
    }
}
