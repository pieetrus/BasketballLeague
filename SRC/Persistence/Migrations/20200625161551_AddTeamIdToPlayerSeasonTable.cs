using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AddTeamIdToPlayerSeasonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Player_Season_Team_ID",
                table: "Player_Season",
                column: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Season_Team_ID_Season_Team_Team_ID",
                table: "Player_Season",
                column: "Team_ID",
                principalTable: "Team_Season",
                principalColumn: "Team_Season_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Season_Team_ID_Season_Team_Team_ID",
                table: "Player_Season");

            migrationBuilder.DropIndex(
                name: "IX_Player_Season_Team_ID",
                table: "Player_Season");
        }
    }
}
