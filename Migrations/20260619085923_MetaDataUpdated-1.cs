using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainee_management.Migrations
{
    /// <inheritdoc />
    public partial class MetaDataUpdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "Metadata",
                newName: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Metadata",
                newName: "timestamp");
        }
    }
}
