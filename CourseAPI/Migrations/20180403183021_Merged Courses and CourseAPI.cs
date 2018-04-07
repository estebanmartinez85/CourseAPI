using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseAPI.Migrations
{
    public partial class MergedCoursesandCourseAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slide");

            migrationBuilder.RenameColumn(
                name: "SlidesReadyToWork",
                table: "Storyboards",
                newName: "ReadyForReview");

            migrationBuilder.AddColumn<int>(
                name: "SlidesId",
                table: "Storyboards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SlideCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlidesStr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideCollection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storyboards_SlidesId",
                table: "Storyboards",
                column: "SlidesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storyboards_SlideCollection_SlidesId",
                table: "Storyboards",
                column: "SlidesId",
                principalTable: "SlideCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storyboards_SlideCollection_SlidesId",
                table: "Storyboards");

            migrationBuilder.DropTable(
                name: "SlideCollection");

            migrationBuilder.DropIndex(
                name: "IX_Storyboards_SlidesId",
                table: "Storyboards");

            migrationBuilder.DropColumn(
                name: "SlidesId",
                table: "Storyboards");

            migrationBuilder.RenameColumn(
                name: "ReadyForReview",
                table: "Storyboards",
                newName: "SlidesReadyToWork");

            migrationBuilder.CreateTable(
                name: "Slide",
                columns: table => new
                {
                    SlideId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Audio = table.Column<string>(nullable: true),
                    Complete = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    GraphicNote = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    StoryboardId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    WriterNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slide", x => x.SlideId);
                    table.ForeignKey(
                        name: "FK_Slide_Storyboards_StoryboardId",
                        column: x => x.StoryboardId,
                        principalTable: "Storyboards",
                        principalColumn: "StoryboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slide_StoryboardId",
                table: "Slide",
                column: "StoryboardId");
        }
    }
}
