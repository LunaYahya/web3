using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_learninig.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseId", "Credits", "Title" },
                values: new object[,]
                {
                    { 1, 3, "Math " },
                    { 2, 4, "Physics " },
                    { 3, 3, "Programming C#" },
                    { 4, 2, "English" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "ahmad@student.com", "Ahmad", "Ali", "123456" },
                    { 2, "sara@student.com", "Sara", "Mohammad", "123456" },
                    { 3, "omar@student.com", "Omar", "Hassan", "123456" },
                    { 4, "laila@student.com", "Laila", "Mahmoud", "123456" }
                });

            migrationBuilder.InsertData(
                table: "Enrollment",
                columns: new[] { "EnrollmentId", "CourseId", "EnrollmentDate", "Grade", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", 1 },
                    { 2, 3, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "B+", 1 },
                    { 3, 2, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A-", 2 },
                    { 4, 4, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", 2 },
                    { 5, 1, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "C+", 3 },
                    { 6, 3, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId",
                table: "Enrollment",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
