using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class PictureUpdatev1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QuestionImage",
                table: "Question",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnswerPicture",
                table: "Answer",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "QuestionImage",
                table: "Question",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "AnswerPicture",
                table: "Answer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
