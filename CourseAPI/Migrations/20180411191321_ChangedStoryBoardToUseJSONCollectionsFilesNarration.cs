using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseAPI.Migrations
{
    public partial class ChangedStoryBoardToUseJSONCollectionsFilesNarration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SlidesNarrationComplete",
                table: "Storyboards",
                newName: "NarrationReadyForReview");

            migrationBuilder.RenameColumn(
                name: "SlidesGraphicsComplete",
                table: "Storyboards",
                newName: "NarrationComplete");

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "Storyboards",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GraphicsComplete",
                table: "Storyboards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GraphicsReadyForReview",
                table: "Storyboards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GraphicsStr",
                table: "Storyboards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NarrationStr",
                table: "Storyboards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "Storyboards");

            migrationBuilder.DropColumn(
                name: "GraphicsComplete",
                table: "Storyboards");

            migrationBuilder.DropColumn(
                name: "GraphicsReadyForReview",
                table: "Storyboards");

            migrationBuilder.DropColumn(
                name: "GraphicsStr",
                table: "Storyboards");

            migrationBuilder.DropColumn(
                name: "NarrationStr",
                table: "Storyboards");

            migrationBuilder.RenameColumn(
                name: "NarrationReadyForReview",
                table: "Storyboards",
                newName: "SlidesNarrationComplete");

            migrationBuilder.RenameColumn(
                name: "NarrationComplete",
                table: "Storyboards",
                newName: "SlidesGraphicsComplete");

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
    }
}
