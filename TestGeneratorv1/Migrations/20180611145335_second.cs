using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGeneratorv1.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Predmet_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Predmet",
                table: "Predmet");

            migrationBuilder.RenameTable(
                name: "Predmet",
                newName: "Question");

            migrationBuilder.AddColumn<byte[]>(
                name: "AnswerPicture",
                table: "Answer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerText",
                table: "Answer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevelId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPointsId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionTypeId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisibilityId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "QuestionId");

            migrationBuilder.CreateTable(
                name: "NumberOfPoints",
                columns: table => new
                {
                    NumberOfPointsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Points = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfPoints", x => x.NumberOfPointsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_AnswerId",
                table: "Question",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_DifficultyLevelId",
                table: "Question",
                column: "DifficultyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_NumberOfPointsId",
                table: "Question",
                column: "NumberOfPointsId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_StatusId",
                table: "Question",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_SubjectId",
                table: "Question",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_VisibilityId",
                table: "Question",
                column: "VisibilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Answer_AnswerId",
                table: "Question",
                column: "AnswerId",
                principalTable: "Answer",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_DifficultyLevel_DifficultyLevelId",
                table: "Question",
                column: "DifficultyLevelId",
                principalTable: "DifficultyLevel",
                principalColumn: "DifficultyLevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_NumberOfPoints_NumberOfPointsId",
                table: "Question",
                column: "NumberOfPointsId",
                principalTable: "NumberOfPoints",
                principalColumn: "NumberOfPointsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionType_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId",
                principalTable: "QuestionType",
                principalColumn: "QuestionTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Status_StatusId",
                table: "Question",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Subject_SubjectId",
                table: "Question",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Visibility_VisibilityId",
                table: "Question",
                column: "VisibilityId",
                principalTable: "Visibility",
                principalColumn: "VisibilityId",
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
                name: "FK_Question_Answer_AnswerId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_DifficultyLevel_DifficultyLevelId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_NumberOfPoints_NumberOfPointsId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionType_QuestionTypeId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Status_StatusId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Subject_SubjectId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Visibility_VisibilityId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Question_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropTable(
                name: "NumberOfPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_AnswerId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_DifficultyLevelId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_NumberOfPointsId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_StatusId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_SubjectId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_VisibilityId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "AnswerPicture",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "AnswerText",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "DifficultyLevelId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "NumberOfPointsId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionTypeId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "VisibilityId",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Predmet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Predmet",
                table: "Predmet",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Predmet_QuestionId",
                table: "TestQuestion",
                column: "QuestionId",
                principalTable: "Predmet",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
