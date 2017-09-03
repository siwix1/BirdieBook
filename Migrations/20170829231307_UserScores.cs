using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BirdieBook.Migrations
{
    public partial class UserScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRound",
                columns: table => new
                {
                    UserRoundID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DailyScratchRating = table.Column<int>(type: "int", nullable: false),
                    TeeBoxID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserHCP = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherCondition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRound", x => x.UserRoundID);
                });

            migrationBuilder.CreateTable(
                name: "UserScore",
                columns: table => new
                {
                    UserScoreID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FairwayHit = table.Column<bool>(type: "bit", nullable: false),
                    HoleID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PuttCount = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserRoundID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScore", x => x.UserScoreID);
                    table.ForeignKey(
                        name: "FK_UserScore_UserRound_UserRoundID",
                        column: x => x.UserRoundID,
                        principalTable: "UserRound",
                        principalColumn: "UserRoundID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserScore_UserRoundID",
                table: "UserScore",
                column: "UserRoundID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserScore");

            migrationBuilder.DropTable(
                name: "UserRound");
        }
    }
}
