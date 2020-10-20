using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class RemoveCoachTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foul_Coach_ID_Coach_Coach_ID",
                table: "Foul");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Season_Coach_ID_Coach_Coach_ID",
                table: "Team_Season");

            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropIndex(
                name: "IX_Team_Season_Coach_ID",
                table: "Team_Season");

            migrationBuilder.DropIndex(
                name: "IX_Foul_Coach_ID",
                table: "Foul");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    Coach_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Photo_URL = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Coach_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_Season_Coach_ID",
                table: "Team_Season",
                column: "Coach_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Foul_Coach_ID",
                table: "Foul",
                column: "Coach_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Foul_Coach_ID_Coach_Coach_ID",
                table: "Foul",
                column: "Coach_ID",
                principalTable: "Coach",
                principalColumn: "Coach_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Season_Coach_ID_Coach_Coach_ID",
                table: "Team_Season",
                column: "Coach_ID",
                principalTable: "Coach",
                principalColumn: "Coach_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
