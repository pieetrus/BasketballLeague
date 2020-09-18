using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class RemoveCollectionOfFreeThrowsAndReplaceItWithOneToOneRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Rebound_ReboundId",
                table: "Incident");

            migrationBuilder.DropIndex(
                name: "IX_Incident_ReboundId",
                table: "Incident");

            migrationBuilder.DropIndex(
                name: "IX_Free_Throw_Foul_ID",
                table: "Free_Throw");

            migrationBuilder.DropColumn(
                name: "ReboundId",
                table: "Incident");

            migrationBuilder.CreateIndex(
                name: "IX_Free_Throw_Foul_ID",
                table: "Free_Throw",
                column: "Foul_ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Free_Throw_Foul_ID",
                table: "Free_Throw");

            migrationBuilder.AddColumn<int>(
                name: "ReboundId",
                table: "Incident",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incident_ReboundId",
                table: "Incident",
                column: "ReboundId");

            migrationBuilder.CreateIndex(
                name: "IX_Free_Throw_Foul_ID",
                table: "Free_Throw",
                column: "Foul_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_Rebound_ReboundId",
                table: "Incident",
                column: "ReboundId",
                principalTable: "Rebound",
                principalColumn: "Rebound_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
