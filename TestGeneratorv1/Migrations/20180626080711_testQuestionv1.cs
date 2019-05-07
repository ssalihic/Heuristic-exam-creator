using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class testQuestionv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "TestQuestion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Test_TestId",
                table: "TestQuestion");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestQuestion");
        }
    }
}
