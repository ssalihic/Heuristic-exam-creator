using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class updatev4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "TestQuestion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "TestQuestion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "TestQuestion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "TestQuestion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
