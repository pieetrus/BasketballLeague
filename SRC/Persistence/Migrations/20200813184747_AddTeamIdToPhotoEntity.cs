using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AddTeamIdToPhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo_URL",
                table: "Team");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Photos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_TeamId",
                table: "Photos",
                column: "TeamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Team_TeamId",
                table: "Photos",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Team_TeamId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_TeamId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "Logo_URL",
                table: "Team",
                type: "varchar(300)",
                unicode: false,
                maxLength: 300,
                nullable: true);
        }
    }
}
