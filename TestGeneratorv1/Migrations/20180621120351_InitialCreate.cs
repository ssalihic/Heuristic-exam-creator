using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "QuestionImage",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "Question",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionImage",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "Question");
        }
    }
}
