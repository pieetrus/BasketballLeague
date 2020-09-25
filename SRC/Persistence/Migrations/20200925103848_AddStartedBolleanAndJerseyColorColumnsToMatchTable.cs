using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AddStartedBolleanAndJerseyColorColumnsToMatchTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Started",
                table: "Match",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TeamGuestJerseyColor",
                table: "Match",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamHomeJerseyColor",
                table: "Match",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Started",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "TeamGuestJerseyColor",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "TeamHomeJerseyColor",
                table: "Match");
        }
    }
}
