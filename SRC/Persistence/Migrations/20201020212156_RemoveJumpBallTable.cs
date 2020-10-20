using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class RemoveJumpBallTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jump_Ball");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jump_Ball",
                columns: table => new
                {
                    Jump_Ball_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(type: "int", nullable: false),
                    Jump_Ball_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jump_Ball", x => x.Jump_Ball_ID);
                    table.ForeignKey(
                        name: "FK_Jump_Ball_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jump_Ball_Incident_ID",
                table: "Jump_Ball",
                column: "Incident_ID",
                unique: true);
        }
    }
}
