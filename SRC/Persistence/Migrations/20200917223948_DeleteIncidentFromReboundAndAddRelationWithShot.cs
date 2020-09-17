using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class DeleteIncidentFromReboundAndAddRelationWithShot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rebound_Incident_ID_Incident_Incident_ID",
                table: "Rebound");

            migrationBuilder.DropIndex(
                name: "UQ_Rebound_Incident_ID",
                table: "Rebound");

            migrationBuilder.DropColumn(
                name: "Incident_ID",
                table: "Rebound");

            migrationBuilder.AddColumn<int>(
                name: "FreeThrowId",
                table: "Rebound",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShotId",
                table: "Rebound",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReboundId",
                table: "Incident",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rebound_FreeThrowId",
                table: "Rebound",
                column: "FreeThrowId",
                unique: true,
                filter: "[FreeThrowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Rebound_ShotId",
                table: "Rebound",
                column: "ShotId",
                unique: true,
                filter: "[ShotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_ReboundId",
                table: "Incident",
                column: "ReboundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_Rebound_ReboundId",
                table: "Incident",
                column: "ReboundId",
                principalTable: "Rebound",
                principalColumn: "Rebound_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rebound_Free_Throw_FreeThrowId",
                table: "Rebound",
                column: "FreeThrowId",
                principalTable: "Free_Throw",
                principalColumn: "Free_Throw_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rebound_Shot_ShotId",
                table: "Rebound",
                column: "ShotId",
                principalTable: "Shot",
                principalColumn: "Shot_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Rebound_ReboundId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Rebound_Free_Throw_FreeThrowId",
                table: "Rebound");

            migrationBuilder.DropForeignKey(
                name: "FK_Rebound_Shot_ShotId",
                table: "Rebound");

            migrationBuilder.DropIndex(
                name: "IX_Rebound_FreeThrowId",
                table: "Rebound");

            migrationBuilder.DropIndex(
                name: "IX_Rebound_ShotId",
                table: "Rebound");

            migrationBuilder.DropIndex(
                name: "IX_Incident_ReboundId",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "FreeThrowId",
                table: "Rebound");

            migrationBuilder.DropColumn(
                name: "ShotId",
                table: "Rebound");

            migrationBuilder.DropColumn(
                name: "ReboundId",
                table: "Incident");

            migrationBuilder.AddColumn<int>(
                name: "Incident_ID",
                table: "Rebound",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UQ_Rebound_Incident_ID",
                table: "Rebound",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rebound_Incident_ID_Incident_Incident_ID",
                table: "Rebound",
                column: "Incident_ID",
                principalTable: "Incident",
                principalColumn: "Incident_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
