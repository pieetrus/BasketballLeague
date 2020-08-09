using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballLeague.Persistence.Migrations
{
    public partial class RemoveEndDateFromMatchTableAndAddEndedBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    Coach_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    Photo_URL = table.Column<string>(unicode: false, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Coach_ID);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    Division_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Short_Name = table.Column<string>(maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Division_ID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Player_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    Photo_URL = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    Height = table.Column<int>(nullable: true),
                    Position = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Player_ID);
                });

            migrationBuilder.CreateTable(
                name: "Referee",
                columns: table => new
                {
                    Referee_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    Jersey_Nr = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: true),
                    Photo_URL = table.Column<string>(unicode: false, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee", x => x.Referee_ID);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Season_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Start_date = table.Column<DateTime>(type: "date", nullable: false),
                    End_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Season_ID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Team_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Short_Name = table.Column<string>(unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    Logo_URL = table.Column<string>(unicode: false, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Team_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 35, nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    Bio = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Player_ID");
                });

            migrationBuilder.CreateTable(
                name: "Season_Division",
                columns: table => new
                {
                    Season_Division_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season_ID = table.Column<int>(nullable: false),
                    Division_ID = table.Column<int>(nullable: false),
                    Winner_Season_Division_Team_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season_Division", x => x.Season_Division_ID);
                    table.ForeignKey(
                        name: "FK_Season_Division_Division_ID_Division_Division_ID",
                        column: x => x.Division_ID,
                        principalTable: "Division",
                        principalColumn: "Division_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Season_Division_Season_ID_Season_Season_ID",
                        column: x => x.Season_ID,
                        principalTable: "Season",
                        principalColumn: "Season_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Winner_Season_Division_Team_Division_ID_Division_Division_ID",
                        column: x => x.Winner_Season_Division_Team_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Match_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season_Division_ID = table.Column<int>(nullable: false),
                    Team_Home_ID = table.Column<int>(nullable: false),
                    Team_Guest_ID = table.Column<int>(nullable: false),
                    Attendance = table.Column<int>(nullable: false),
                    Start_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Ended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Match_ID);
                    table.ForeignKey(
                        name: "FK_Match_Season_Division_ID_Season_Division_Season_Division_ID",
                        column: x => x.Season_Division_ID,
                        principalTable: "Season_Division",
                        principalColumn: "Season_Division_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_Team_Guest_ID_Team_Team_ID",
                        column: x => x.Team_Guest_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_Home_ID_Team_Team_ID",
                        column: x => x.Team_Home_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team_Season",
                columns: table => new
                {
                    Team_Season_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team_ID = table.Column<int>(nullable: false),
                    Season_Division_ID = table.Column<int>(nullable: false),
                    PTS = table.Column<int>(nullable: true, computedColumnSql: "(((2)*[FG2M]+(3)*[FG3M])+[FTM])"),
                    FGA = table.Column<int>(nullable: true, computedColumnSql: "([FG3A]+[FG2A])"),
                    FGM = table.Column<int>(nullable: true, computedColumnSql: "([FG3M]+[FG2M])"),
                    FG3A = table.Column<int>(nullable: false),
                    FG3M = table.Column<int>(nullable: false),
                    FG2A = table.Column<int>(nullable: false),
                    FG2M = table.Column<int>(nullable: false),
                    FTA = table.Column<int>(nullable: false),
                    FTM = table.Column<int>(nullable: false),
                    TRB = table.Column<int>(nullable: true, computedColumnSql: "([ORB]+[DRB])"),
                    ORB = table.Column<int>(nullable: false),
                    DRB = table.Column<int>(nullable: false),
                    AST = table.Column<int>(nullable: false),
                    STL = table.Column<int>(nullable: false),
                    BLK = table.Column<int>(nullable: false),
                    TOV = table.Column<int>(nullable: false),
                    FOULS = table.Column<int>(nullable: false),
                    OFF_FOULS = table.Column<int>(nullable: false),
                    Coach_ID = table.Column<int>(nullable: true),
                    Capitain_ID = table.Column<int>(nullable: true),
                    Ranking_points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Season", x => x.Team_Season_ID);
                    table.ForeignKey(
                        name: "FK_Team_Season_Capitain_ID_Player_Player_ID",
                        column: x => x.Capitain_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Season_Coach_ID_Coach_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Season_Season_Division_ID_Season_Division_ID",
                        column: x => x.Season_Division_ID,
                        principalTable: "Season_Division",
                        principalColumn: "Season_Division_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Season_Team_ID_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Incident_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Match_ID = table.Column<int>(nullable: false),
                    Minutes = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Seconds = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    Incident_type = table.Column<int>(nullable: false),
                    Quater = table.Column<int>(nullable: false),
                    Flagged = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Incident_ID);
                    table.ForeignKey(
                        name: "FK_Incident_Match_ID_Match_Match_ID",
                        column: x => x.Match_ID,
                        principalTable: "Match",
                        principalColumn: "Match_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player_Match",
                columns: table => new
                {
                    Player_Match_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player_ID = table.Column<int>(nullable: false),
                    Match_ID = table.Column<int>(nullable: false),
                    PTS = table.Column<int>(nullable: true, computedColumnSql: "(((2)*[FG2M]+(3)*[FG3M])+[FTM])"),
                    FGA = table.Column<int>(nullable: true, computedColumnSql: "([FG3A]+[FG2A])"),
                    FGM = table.Column<int>(nullable: true, computedColumnSql: "([FG3M]+[FG2M])"),
                    FG3A = table.Column<int>(nullable: false),
                    FG3M = table.Column<int>(nullable: false),
                    FG2A = table.Column<int>(nullable: false),
                    FG2M = table.Column<int>(nullable: false),
                    FTA = table.Column<int>(nullable: false),
                    FTM = table.Column<int>(nullable: false),
                    TRB = table.Column<int>(nullable: true, computedColumnSql: "([ORB]+[DRB])"),
                    ORB = table.Column<int>(nullable: false),
                    DRB = table.Column<int>(nullable: false),
                    AST = table.Column<int>(nullable: false),
                    STL = table.Column<int>(nullable: false),
                    BLK = table.Column<int>(nullable: false),
                    TOV = table.Column<int>(nullable: false),
                    FOULS = table.Column<int>(nullable: false),
                    OFF_FOULS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Match", x => x.Player_Match_ID);
                    table.ForeignKey(
                        name: "FK_Player_Match_Match_ID_Match_ID",
                        column: x => x.Match_ID,
                        principalTable: "Match",
                        principalColumn: "Match_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Match_Player_ID_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Referee_Matches",
                columns: table => new
                {
                    Referee_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Match_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee_Matches_Referee_ID_Match_ID", x => new { x.Referee_ID, x.Match_ID });
                    table.ForeignKey(
                        name: "FK_Referee_Matches_Referee_ID_Referee_Referee_ID",
                        column: x => x.Referee_ID,
                        principalTable: "Referee",
                        principalColumn: "Referee_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referee_Matches_Match_ID_Match_Match_ID",
                        column: x => x.Match_ID,
                        principalTable: "Match",
                        principalColumn: "Match_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team_Match",
                columns: table => new
                {
                    Team_Match_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team_ID = table.Column<int>(nullable: false),
                    Match_ID = table.Column<int>(nullable: false),
                    PTS = table.Column<int>(nullable: true, computedColumnSql: "(((2)*[FG2M]+(3)*[FG3M])+[FTM])"),
                    FGA = table.Column<int>(nullable: true, computedColumnSql: "([FG3A]+[FG2A])"),
                    FGM = table.Column<int>(nullable: true, computedColumnSql: "([FG3M]+[FG2M])"),
                    FG3A = table.Column<int>(nullable: false),
                    FG3M = table.Column<int>(nullable: false),
                    FG2A = table.Column<int>(nullable: false),
                    FG2M = table.Column<int>(nullable: false),
                    FTA = table.Column<int>(nullable: false),
                    FTM = table.Column<int>(nullable: false),
                    TRB = table.Column<int>(nullable: true, computedColumnSql: "([ORB]+[DRB])"),
                    ORB = table.Column<int>(nullable: false),
                    DRB = table.Column<int>(nullable: false),
                    AST = table.Column<int>(nullable: false),
                    STL = table.Column<int>(nullable: false),
                    BLK = table.Column<int>(nullable: false),
                    TOV = table.Column<int>(nullable: false),
                    FOULS = table.Column<int>(nullable: false),
                    BENCH_PTS = table.Column<int>(nullable: false),
                    FASTBREAKPOINTS = table.Column<int>(nullable: false),
                    SECOND_CHANCE_POINTS = table.Column<int>(nullable: false),
                    POINTS_FROM_TURNOVERS = table.Column<int>(nullable: false),
                    OFF_FOULS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Match", x => x.Team_Match_ID);
                    table.ForeignKey(
                        name: "FK_Team_Match_Match_ID_Match_ID",
                        column: x => x.Match_ID,
                        principalTable: "Match",
                        principalColumn: "Match_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Match_Team_ID_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player_Season",
                columns: table => new
                {
                    Player_Season = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player_ID = table.Column<int>(nullable: false),
                    Season_Division_ID = table.Column<int>(nullable: false),
                    Team_ID = table.Column<int>(nullable: true),
                    Jersey_Nr = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    PTS = table.Column<int>(nullable: true, computedColumnSql: "(((2)*[FG2M]+(3)*[FG3M])+[FTM])"),
                    FGA = table.Column<int>(nullable: true, computedColumnSql: "([FG3A]+[FG2A])"),
                    FGM = table.Column<int>(nullable: true, computedColumnSql: "([FG3M]+[FG2M])"),
                    FG3A = table.Column<int>(nullable: false),
                    FG3M = table.Column<int>(nullable: false),
                    FG2A = table.Column<int>(nullable: false),
                    FG2M = table.Column<int>(nullable: false),
                    FTA = table.Column<int>(nullable: false),
                    FTM = table.Column<int>(nullable: false),
                    TRB = table.Column<int>(nullable: true, computedColumnSql: "([ORB]+[DRB])"),
                    ORB = table.Column<int>(nullable: false),
                    DRB = table.Column<int>(nullable: false),
                    AST = table.Column<int>(nullable: false),
                    STL = table.Column<int>(nullable: false),
                    BLK = table.Column<int>(nullable: false),
                    TOV = table.Column<int>(nullable: false),
                    FOULS = table.Column<int>(nullable: false),
                    OFF_FOULS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Season_Player_Season_ID", x => x.Player_Season);
                    table.ForeignKey(
                        name: "FK_Player_Season_Player_ID_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Season_Season_Division_ID_Season_Division_ID",
                        column: x => x.Season_Division_ID,
                        principalTable: "Season_Division",
                        principalColumn: "Season_Division_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_Season_Team_ID_Season_Team_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Team_Season",
                        principalColumn: "Team_Season_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Foul",
                columns: table => new
                {
                    Foul_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Player_Who_Fouled_ID = table.Column<int>(nullable: true),
                    Player_Who_Was_Fouled_ID = table.Column<int>(nullable: true),
                    Coach_ID = table.Column<int>(nullable: true),
                    Team_ID = table.Column<int>(nullable: true),
                    Foul_Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foul", x => x.Foul_ID);
                    table.ForeignKey(
                        name: "FK_Foul_Coach_ID_Coach_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foul_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foul_Player_Who_Fouled_ID_Player_Player_ID",
                        column: x => x.Player_Who_Fouled_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foul_Player_Who_Was_Fouled_ID_Player_Player_ID",
                        column: x => x.Player_Who_Was_Fouled_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foul_Team_ID_Team_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jump_Ball",
                columns: table => new
                {
                    Jump_Ball_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Jump_Ball_Type = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Rebound",
                columns: table => new
                {
                    Rebound_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Rebound_Type = table.Column<int>(nullable: false),
                    Player_ID = table.Column<int>(nullable: true),
                    Team_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rebound", x => x.Rebound_ID);
                    table.ForeignKey(
                        name: "FK_Rebound_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rebound_Player_ID_Player_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rebound_Team_ID_Team_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shot",
                columns: table => new
                {
                    Shot_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Player_ID = table.Column<int>(nullable: false),
                    Shot_Type = table.Column<int>(nullable: false),
                    Is_Accurate = table.Column<bool>(nullable: false),
                    Is_Fast_Attack = table.Column<bool>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shot", x => x.Shot_ID);
                    table.ForeignKey(
                        name: "FK_Shot_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shot_Player_ID_Player_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Substitution",
                columns: table => new
                {
                    Substitution_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Player_IN_ID = table.Column<int>(nullable: false),
                    Player_OUT_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitution", x => x.Substitution_ID);
                    table.ForeignKey(
                        name: "FK_Substitution_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitution_Player_IN_ID_Player_Player_ID",
                        column: x => x.Player_IN_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Substitution_Player_OUT_ID_Player_Player_ID",
                        column: x => x.Player_OUT_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Timeout",
                columns: table => new
                {
                    Timeout_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Team_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeout", x => x.Timeout_ID);
                    table.ForeignKey(
                        name: "FK_Timeout_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timeout_Team_ID_Team_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Team",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turnover",
                columns: table => new
                {
                    Turnover_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Incident_ID = table.Column<int>(nullable: false),
                    Player_ID = table.Column<int>(nullable: false),
                    Turnover_Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnover", x => x.Turnover_ID);
                    table.ForeignKey(
                        name: "FK_Turnover_Incident_ID_Incident_Incident_ID",
                        column: x => x.Incident_ID,
                        principalTable: "Incident",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnover_Player_ID_Player_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Free_Throw",
                columns: table => new
                {
                    Free_Throw_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Foul_ID = table.Column<int>(nullable: false),
                    Player_Shooter_ID = table.Column<int>(nullable: false),
                    AccurateShots = table.Column<int>(nullable: false),
                    Attempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Free_Throw", x => x.Free_Throw_ID);
                    table.ForeignKey(
                        name: "FK_Free_Throw_Foul_ID_Foul_Foul_ID",
                        column: x => x.Foul_ID,
                        principalTable: "Foul",
                        principalColumn: "Foul_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Free_Throw_Player_Shooter_ID_Player_Player_ID",
                        column: x => x.Player_Shooter_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Block_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shot_ID = table.Column<int>(nullable: false),
                    Player_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Block_ID);
                    table.ForeignKey(
                        name: "FK_Block_Player_ID_Player_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Block_Shot_ID_Shot_Shot_ID",
                        column: x => x.Shot_ID,
                        principalTable: "Shot",
                        principalColumn: "Shot_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steal",
                columns: table => new
                {
                    Steal_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turnover_ID = table.Column<int>(nullable: false),
                    Player_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steal", x => x.Steal_ID);
                    table.ForeignKey(
                        name: "FK_Steal_Player_ID_Player_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Steal_Turnover_ID_Turnover_Turnover_ID",
                        column: x => x.Turnover_ID,
                        principalTable: "Turnover",
                        principalColumn: "Turnover_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assist",
                columns: table => new
                {
                    Assist_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shot_ID = table.Column<int>(nullable: true),
                    Free_Throw_ID = table.Column<int>(nullable: true),
                    Player_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assist", x => x.Assist_ID);
                    table.ForeignKey(
                        name: "FK_Assist_Free_Throw_ID_Free_Throw_Free_Throw_ID",
                        column: x => x.Free_Throw_ID,
                        principalTable: "Free_Throw",
                        principalColumn: "Free_Throw_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assist_Player_ID_Player_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Player",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assist_Shot_ID_Shot_Shot_ID",
                        column: x => x.Shot_ID,
                        principalTable: "Shot",
                        principalColumn: "Shot_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlayerId",
                table: "AspNetUsers",
                column: "PlayerId",
                unique: true,
                filter: "[PlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assist_Free_Throw_ID",
                table: "Assist",
                column: "Free_Throw_ID",
                unique: true,
                filter: "[Free_Throw_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assist_Player_ID",
                table: "Assist",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Assist_Shot_ID",
                table: "Assist",
                column: "Shot_ID",
                unique: true,
                filter: "[Shot_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Block_Player_ID",
                table: "Block",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Block_Shot_ID",
                table: "Block",
                column: "Shot_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foul_Coach_ID",
                table: "Foul",
                column: "Coach_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Foul_Incident_ID",
                table: "Foul",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foul_Player_Who_Fouled_ID",
                table: "Foul",
                column: "Player_Who_Fouled_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Foul_Player_Who_Was_Fouled_ID",
                table: "Foul",
                column: "Player_Who_Was_Fouled_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Foul_Team_ID",
                table: "Foul",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Free_Throw_Foul_ID",
                table: "Free_Throw",
                column: "Foul_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Free_Throw_Player_Shooter_ID",
                table: "Free_Throw",
                column: "Player_Shooter_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_Match_ID",
                table: "Incident",
                column: "Match_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Jump_Ball_Incident_ID",
                table: "Jump_Ball",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_Season_Division_ID",
                table: "Match",
                column: "Season_Division_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team_Guest_ID",
                table: "Match",
                column: "Team_Guest_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team_Home_ID",
                table: "Match",
                column: "Team_Home_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Match_Match_ID",
                table: "Player_Match",
                column: "Match_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Player_Match_Player_ID_Match_ID",
                table: "Player_Match",
                columns: new[] { "Player_ID", "Match_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_Season_Player_ID",
                table: "Player_Season",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Season_Season_Division_ID",
                table: "Player_Season",
                column: "Season_Division_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Season_Team_ID",
                table: "Player_Season",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Rebound_Incident_ID",
                table: "Rebound",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rebound_Player_ID",
                table: "Rebound",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rebound_Team_ID",
                table: "Rebound",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_Matches_Match_ID",
                table: "Referee_Matches",
                column: "Match_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Season_Division_Division_ID",
                table: "Season_Division",
                column: "Division_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Season_Division_Season_ID",
                table: "Season_Division",
                column: "Season_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Season_Division_Winner_Season_Division_Team_ID",
                table: "Season_Division",
                column: "Winner_Season_Division_Team_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Shot_Incident_ID",
                table: "Shot",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shot_Player_ID",
                table: "Shot",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Steal_Player_ID",
                table: "Steal",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Steal_Turnover_ID",
                table: "Steal",
                column: "Turnover_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Substitution_Incident_ID",
                table: "Substitution",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Substitution_Player_IN_ID",
                table: "Substitution",
                column: "Player_IN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Substitution_Player_OUT_ID",
                table: "Substitution",
                column: "Player_OUT_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Team_Name",
                table: "Team",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_Match_Match_ID",
                table: "Team_Match",
                column: "Match_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Team_Match_Team_ID_Match_ID",
                table: "Team_Match",
                columns: new[] { "Team_ID", "Match_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_Season_Capitain_ID",
                table: "Team_Season",
                column: "Capitain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Season_Coach_ID",
                table: "Team_Season",
                column: "Coach_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Season_Season_Division_ID",
                table: "Team_Season",
                column: "Season_Division_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Team_Season_Player_ID_Season_Division_ID",
                table: "Team_Season",
                columns: new[] { "Team_ID", "Season_Division_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Timeout_Incident_ID",
                table: "Timeout",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timeout_Team_ID",
                table: "Timeout",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Turnover_Incident_ID",
                table: "Turnover",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnover_Player_ID",
                table: "Turnover",
                column: "Player_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Assist");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Jump_Ball");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Player_Match");

            migrationBuilder.DropTable(
                name: "Player_Season");

            migrationBuilder.DropTable(
                name: "Rebound");

            migrationBuilder.DropTable(
                name: "Referee_Matches");

            migrationBuilder.DropTable(
                name: "Steal");

            migrationBuilder.DropTable(
                name: "Substitution");

            migrationBuilder.DropTable(
                name: "Team_Match");

            migrationBuilder.DropTable(
                name: "Timeout");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Free_Throw");

            migrationBuilder.DropTable(
                name: "Shot");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Team_Season");

            migrationBuilder.DropTable(
                name: "Referee");

            migrationBuilder.DropTable(
                name: "Turnover");

            migrationBuilder.DropTable(
                name: "Foul");

            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Season_Division");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
