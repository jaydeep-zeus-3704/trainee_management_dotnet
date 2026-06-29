using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainee_management.Migrations
{
    /// <inheritdoc />
    public partial class MetaDataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeneratedStorageName",
                table: "Metadata",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "Metadata",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedStorageName",
                table: "Metadata");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "Metadata");
        }
    }
}
