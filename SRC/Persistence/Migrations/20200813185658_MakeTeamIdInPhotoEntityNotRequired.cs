using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class MakeTeamIdInPhotoEntityNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Team_TeamId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_TeamId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_TeamId",
                table: "Photos",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Team_TeamId",
                table: "Photos",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Team_TeamId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_TeamId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
