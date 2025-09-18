using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infractructure.Migrations
{
    /// <inheritdoc />
    public partial class DocumentEntityUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Documents");
        }
    }
}
