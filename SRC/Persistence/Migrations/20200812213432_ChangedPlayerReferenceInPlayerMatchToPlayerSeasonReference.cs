using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class ChangedPlayerReferenceInPlayerMatchToPlayerSeasonReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Match_Player_ID_Player_ID",
                table: "Player_Match");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Match_Player_ID_Player_ID",
                table: "Player_Match",
                column: "Player_ID",
                principalTable: "Player_Season",
                principalColumn: "Player_Season",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Match_Player_ID_Player_ID",
                table: "Player_Match");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Match_Player_ID_Player_ID",
                table: "Player_Match",
                column: "Player_ID",
                principalTable: "Player",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
