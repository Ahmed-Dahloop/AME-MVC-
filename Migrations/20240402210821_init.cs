using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AME.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeGender = table.Column<int>(type: "int", nullable: false),
                    EmployeeAge = table.Column<int>(type: "int", nullable: false),
                    EmployeePassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeCode);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbyID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentAge = table.Column<int>(type: "int", nullable: false),
                    StudentClass = table.Column<int>(type: "int", nullable: false),
                    StudentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentGender = table.Column<int>(type: "int", nullable: false),
                    StudentPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
             name: "Subjects",
             columns: table => new
             {
                 SubjectID = table.Column<int>(nullable: false)
                     .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                 SubjectName = table.Column<string>(nullable: false),
                 SubjectScore = table.Column<int>(nullable: false, defaultValue: 100)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Subjects", x => x.SubjectID);
             });

           
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectID", "SubjectName" },
                values: new object[,]
                {
            { 1, "Arabic" },
            { 2, "Mathematics" },
            { 3, "Biology" },
            { 4, "English" },
            { 5, "Chemistry" },
            { 6, "History" },
            { 7, "French" },
            { 8, "Physics" },
            { 9, "Geography" }
                });

            migrationBuilder.CreateTable(
                name: "Practices",
                columns: table => new
                {
                    PracticeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    HobbyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.PracticeID);
                    table.ForeignKey(
                        name: "FK_Practices_Hobbies_HobbyID",
                        column: x => x.HobbyID,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Practices_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherAge = table.Column<int>(type: "int", nullable: false),
                    TeacherGender = table.Column<int>(type: "int", nullable: false),
                    TeacherPhoneNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeachingSubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherID);
                    table.ForeignKey(
                        name: "FK_Teachers_Subjects_TeachingSubjectID",
                        column: x => x.TeachingSubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SubjectID",
                table: "Enrollments",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_HobbyID",
                table: "Practices",
                column: "HobbyID");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_StudentID",
                table: "Practices",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TeachingSubjectID",
                table: "Teachers",
                column: "TeachingSubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Practices");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
