using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FavoritesSurvey.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComputedResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputedResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputedResponses_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputedResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responses_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "What's your favorite planet in the solar system?" },
                    { 2, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "What's your favorite type of pet?" },
                    { 3, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "What's your favorite continent?" },
                    { 4, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "What's your favorite game genre?" },
                    { 5, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "What's your favorite music genre?" },
                    { 6, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "What's your favorite big tech company?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name", "QuestionId" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Earth", 1 },
                    { 24, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "MOBA", 4 },
                    { 25, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "RTS", 4 },
                    { 26, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Turn-based Strategy", 4 },
                    { 27, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Roguelikes", 4 },
                    { 28, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Open World", 4 },
                    { 29, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Sports", 4 },
                    { 30, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Simulation", 4 },
                    { 31, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Rock", 5 },
                    { 23, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "MMO", 4 },
                    { 32, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Pop", 5 },
                    { 34, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "RnB", 5 },
                    { 35, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Electronic", 5 },
                    { 36, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Reggae", 5 },
                    { 37, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Microsoft", 6 },
                    { 38, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Apple", 6 },
                    { 39, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Google", 6 },
                    { 40, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Amazon", 6 },
                    { 41, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "IBM", 6 },
                    { 33, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Hip Hop", 5 },
                    { 42, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Intel", 6 },
                    { 22, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "FPS", 4 },
                    { 20, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Oceania", 3 },
                    { 2, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Jupiter", 1 },
                    { 3, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Venus", 1 },
                    { 4, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Mercury", 1 },
                    { 5, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Mars", 1 },
                    { 6, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Saturn", 1 },
                    { 7, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Neptune", 1 },
                    { 8, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Uranus", 1 },
                    { 9, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Cats", 2 },
                    { 21, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Antarctica", 3 },
                    { 10, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Dogs", 2 },
                    { 12, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Fish", 2 },
                    { 13, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Rabbits", 2 },
                    { 14, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Guinea Pigs", 2 },
                    { 15, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Europe", 3 },
                    { 16, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "South America", 3 },
                    { 17, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Africa", 3 },
                    { 18, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Asia", 3 },
                    { 19, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "North America", 3 },
                    { 11, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Birds", 2 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name", "QuestionId" },
                values: new object[] { 43, 0, new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), new DateTime(2021, 2, 3, 1, 57, 4, 128, DateTimeKind.Local).AddTicks(4833), true, 0, "Oracle", 6 });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedResponses_AnswerId",
                table: "ComputedResponses",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedResponses_QuestionId",
                table: "ComputedResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_AnswerId",
                table: "Responses",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_QuestionId",
                table: "Responses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_SurveyId",
                table: "Responses",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputedResponses");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
