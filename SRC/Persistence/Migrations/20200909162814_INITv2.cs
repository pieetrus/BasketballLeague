using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class INITv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_Season_Team_Guest_ID",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_Season_Team_Home_ID",
                table: "Match");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TeamSeasonGuestId",
                table: "Match",
                column: "TeamSeasonGuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TeamSeasonHomeId",
                table: "Match",
                column: "TeamSeasonHomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_Season_TeamSeasonGuestId",
                table: "Match",
                column: "TeamSeasonGuestId",
                principalTable: "Team_Season",
                principalColumn: "Team_Season_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_Season_TeamSeasonHomeId",
                table: "Match",
                column: "TeamSeasonHomeId",
                principalTable: "Team_Season",
                principalColumn: "Team_Season_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_Season_TeamSeasonGuestId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_Season_TeamSeasonHomeId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_TeamSeasonGuestId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_TeamSeasonHomeId",
                table: "Match");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_Season_Team_Guest_ID",
                table: "Match",
                column: "Team_Guest_ID",
                principalTable: "Team_Season",
                principalColumn: "Team_Season_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_Season_Team_Home_ID",
                table: "Match",
                column: "Team_Home_ID",
                principalTable: "Team_Season",
                principalColumn: "Team_Season_ID");
        }
    }
}
