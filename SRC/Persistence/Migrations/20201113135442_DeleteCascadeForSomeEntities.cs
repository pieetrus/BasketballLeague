using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class DeleteCascadeForSomeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rebound_Shot_ShotId",
                table: "Rebound");

            migrationBuilder.AddForeignKey(
                name: "FK_Rebound_Shot_ShotId",
                table: "Rebound",
                column: "ShotId",
                principalTable: "Shot",
                principalColumn: "Shot_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rebound_Shot_ShotId",
                table: "Rebound");

            migrationBuilder.AddForeignKey(
                name: "FK_Rebound_Shot_ShotId",
                table: "Rebound",
                column: "ShotId",
                principalTable: "Shot",
                principalColumn: "Shot_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
