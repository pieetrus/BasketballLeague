using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AddTeamSeasonReferenceToMatchTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Season_Season_Division_ID_Season_Division_ID",
                table: "Team_Season");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Season_Team_ID_Team_ID",
                table: "Team_Season");

            migrationBuilder.DropIndex(
                name: "IX_Match_Team_Guest_ID",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_Team_Home_ID",
                table: "Match");

            migrationBuilder.AddColumn<int>(
                name: "TeamSeasonGuestId",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamSeasonHomeId",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team_Guest_ID",
                table: "Match",
                column: "Team_Guest_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team_Home_ID",
                table: "Match",
                column: "Team_Home_ID",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Season_Season_Division_ID_Season_Division_ID",
                table: "Team_Season",
                column: "Season_Division_ID",
                principalTable: "Season_Division",
                principalColumn: "Season_Division_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Season_Team_ID_Team_ID",
                table: "Team_Season",
                column: "Team_ID",
                principalTable: "Team",
                principalColumn: "Team_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_Season_Team_Guest_ID",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_Season_Team_Home_ID",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Season_Season_Division_ID_Season_Division_ID",
                table: "Team_Season");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Season_Team_ID_Team_ID",
                table: "Team_Season");

            migrationBuilder.DropIndex(
                name: "IX_Match_Team_Guest_ID",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_Team_Home_ID",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "TeamSeasonGuestId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "TeamSeasonHomeId",
                table: "Match");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team_Guest_ID",
                table: "Match",
                column: "Team_Guest_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team_Home_ID",
                table: "Match",
                column: "Team_Home_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Season_Season_Division_ID_Season_Division_ID",
                table: "Team_Season",
                column: "Season_Division_ID",
                principalTable: "Season_Division",
                principalColumn: "Season_Division_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Season_Team_ID_Team_ID",
                table: "Team_Season",
                column: "Team_ID",
                principalTable: "Team",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
