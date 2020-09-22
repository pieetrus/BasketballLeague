using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AddQuaterFoulsAndTimeoutsFromBothHalfToTeamMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fouls1Qtr",
                table: "Team_Match",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fouls2Qtr",
                table: "Team_Match",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fouls3Qtr",
                table: "Team_Match",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fouls4Qtr",
                table: "Team_Match",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Timeouts1Half",
                table: "Team_Match",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Timeouts2Half",
                table: "Team_Match",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fouls1Qtr",
                table: "Team_Match");

            migrationBuilder.DropColumn(
                name: "Fouls2Qtr",
                table: "Team_Match");

            migrationBuilder.DropColumn(
                name: "Fouls3Qtr",
                table: "Team_Match");

            migrationBuilder.DropColumn(
                name: "Fouls4Qtr",
                table: "Team_Match");

            migrationBuilder.DropColumn(
                name: "Timeouts1Half",
                table: "Team_Match");

            migrationBuilder.DropColumn(
                name: "Timeouts2Half",
                table: "Team_Match");
        }
    }
}
