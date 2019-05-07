using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class initialv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "TestGroup",
                table: "Test");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "TestQuestion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Question",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "NumberOfPoints",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestId",
                table: "Question",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Test_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Test_TestId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropIndex(
                name: "IX_Question_TestId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "TestQuestion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "TestQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Test",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Test",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Test",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestGroup",
                table: "Test",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Points",
                table: "NumberOfPoints",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
