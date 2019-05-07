using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class testUpdatev1 : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
