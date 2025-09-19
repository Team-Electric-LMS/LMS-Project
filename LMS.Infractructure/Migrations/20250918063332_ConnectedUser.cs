using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infractructure.Migrations
{
    /// <inheritdoc />
    public partial class ConnectedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "ApplicationUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseTeachers",
                columns: table => new
                {
                    CoursesTaughtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeachersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeachers", x => new { x.CoursesTaughtId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_CourseTeachers_ApplicationUser_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTeachers_Courses_CoursesTaughtId",
                        column: x => x.CoursesTaughtId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CourseId",
                table: "ApplicationUser",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeachers_TeachersId",
                table: "CourseTeachers",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Courses_CourseId",
                table: "ApplicationUser",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Courses_CourseId",
                table: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "CourseTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_CourseId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ApplicationUser");
        }
    }
}
