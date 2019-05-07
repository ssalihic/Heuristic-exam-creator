using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class userV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UserId",
                table: "Subject",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_AspNetUsers_UserId",
                table: "Subject",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_AspNetUsers_UserId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_UserId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
