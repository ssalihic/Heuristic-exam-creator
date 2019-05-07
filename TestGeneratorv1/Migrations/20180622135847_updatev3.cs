using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class updatev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Question",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Question",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Answer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Answer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Answer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_AreaId",
                table: "Question",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Area_AreaId",
                table: "Question",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Area_AreaId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_AreaId",
                table: "Question");

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

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Answer");
        }
    }
}
