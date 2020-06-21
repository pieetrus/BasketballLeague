using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AddTeamIdToFoulTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "Foul",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foul_Team_ID",
                table: "Foul",
                column: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Foul_Team_ID_Team_Team_ID",
                table: "Foul",
                column: "Team_ID",
                principalTable: "Team",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foul_Team_ID_Team_Team_ID",
                table: "Foul");

            migrationBuilder.DropIndex(
                name: "IX_Foul_Team_ID",
                table: "Foul");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "Foul");
        }
    }
}
