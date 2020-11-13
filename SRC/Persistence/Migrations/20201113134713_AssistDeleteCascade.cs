using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class AssistDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assist_Shot_ID_Shot_Shot_ID",
                table: "Assist");

            migrationBuilder.AddForeignKey(
                name: "FK_Assist_Shot_ID_Shot_Shot_ID",
                table: "Assist",
                column: "Shot_ID",
                principalTable: "Shot",
                principalColumn: "Shot_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assist_Shot_ID_Shot_Shot_ID",
                table: "Assist");

            migrationBuilder.AddForeignKey(
                name: "FK_Assist_Shot_ID_Shot_Shot_ID",
                table: "Assist",
                column: "Shot_ID",
                principalTable: "Shot",
                principalColumn: "Shot_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
