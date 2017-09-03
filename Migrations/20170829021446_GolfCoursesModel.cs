using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BirdieBook.Migrations
{
    public partial class GolfCoursesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GolfCourse",
                columns: table => new
                {
                    GolfCourseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfCourse", x => x.GolfCourseID);
                });

            migrationBuilder.CreateTable(
                name: "TeeBox",
                columns: table => new
                {
                    TeeBoxID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GolfCourseID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MensCourseRating = table.Column<int>(type: "int", nullable: false),
                    MensSlope = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WomensCourseRating = table.Column<int>(type: "int", nullable: false),
                    WomensSlope = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeeBox", x => x.TeeBoxID);
                    table.ForeignKey(
                        name: "FK_TeeBox_GolfCourse_GolfCourseID",
                        column: x => x.GolfCourseID,
                        principalTable: "GolfCourse",
                        principalColumn: "GolfCourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hole",
                columns: table => new
                {
                    HoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HCPIndex = table.Column<int>(type: "int", nullable: false),
                    HoleID1 = table.Column<int>(type: "int", nullable: true),
                    HoleNumber = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Par = table.Column<int>(type: "int", nullable: false),
                    TeeBoxID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hole", x => x.HoleID);
                    table.ForeignKey(
                        name: "FK_Hole_Hole_HoleID1",
                        column: x => x.HoleID1,
                        principalTable: "Hole",
                        principalColumn: "HoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hole_TeeBox_TeeBoxID",
                        column: x => x.TeeBoxID,
                        principalTable: "TeeBox",
                        principalColumn: "TeeBoxID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hole_HoleID1",
                table: "Hole",
                column: "HoleID1");

            migrationBuilder.CreateIndex(
                name: "IX_Hole_TeeBoxID",
                table: "Hole",
                column: "TeeBoxID");

            migrationBuilder.CreateIndex(
                name: "IX_TeeBox_GolfCourseID",
                table: "TeeBox",
                column: "GolfCourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hole");

            migrationBuilder.DropTable(
                name: "TeeBox");

            migrationBuilder.DropTable(
                name: "GolfCourse");
        }
    }
}
