using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class RemoveRefereesAndPlayerWithAppUserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Player_PlayerId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Referee_Matches");

            migrationBuilder.DropTable(
                name: "Referee");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PlayerId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Referee",
                columns: table => new
                {
                    Referee_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    Jersey_Nr = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Photo_URL = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee", x => x.Referee_ID);
                });

            migrationBuilder.CreateTable(
                name: "Referee_Matches",
                columns: table => new
                {
                    Referee_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Match_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee_Matches_Referee_ID_Match_ID", x => new { x.Referee_ID, x.Match_ID });
                    table.ForeignKey(
                        name: "FK_Referee_Matches_Referee_ID_Referee_Referee_ID",
                        column: x => x.Referee_ID,
                        principalTable: "Referee",
                        principalColumn: "Referee_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referee_Matches_Match_ID_Match_Match_ID",
                        column: x => x.Match_ID,
                        principalTable: "Match",
                        principalColumn: "Match_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlayerId",
                table: "AspNetUsers",
                column: "PlayerId",
                unique: true,
                filter: "[PlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_Matches_Match_ID",
                table: "Referee_Matches",
                column: "Match_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Player_PlayerId",
                table: "AspNetUsers",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Player_ID");
        }
    }
}
