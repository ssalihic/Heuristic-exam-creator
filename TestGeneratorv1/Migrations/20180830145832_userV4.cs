using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class userV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Test",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_CreatorId",
                table: "Test",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CreatorId",
                table: "Question",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_AspNetUsers_CreatorId",
                table: "Question",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Test_AspNetUsers_CreatorId",
                table: "Test",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_AspNetUsers_CreatorId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_AspNetUsers_CreatorId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_CreatorId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Question_CreatorId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
