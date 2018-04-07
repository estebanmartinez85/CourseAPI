using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class UpdatedCoursewithnewflags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StoryboardComplete",
                table: "Courses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WriterMeetingCompleted",
                table: "Courses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "WriterMeetingDate",
                table: "Courses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoryboardComplete",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WriterMeetingCompleted",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WriterMeetingDate",
                table: "Courses");
        }
    }
}
