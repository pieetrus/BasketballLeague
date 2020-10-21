using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballLeague.Persistence
{
    public class Seed
    {
        public static async Task SeedData(IBasketballLeagueDbContext context,
            UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Admin",
                        UserName = "admin",
                        Email = "admin@admin.com"
                    },
                    new AppUser
                    {
                        DisplayName = "User",
                        UserName = "user",
                        Email = "user@user.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                }; // 4

                var adminRole = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };
                var userRole = new IdentityRole { Name = "User", NormalizedName = "USER" };

                context.IdentityRoles.AddRange(adminRole, userRole);

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Haslo1");
                }

                await userManager.AddToRoleAsync(users[0], "ADMIN");
                await userManager.AddToRoleAsync(users[1], "USER");

                await context.SaveChangesAsync();
            }

            if (!context.Player.Any())
            {
                var players = new List<Player>
                {
                    new Player
                    {
                        Name = "Jakub", Surname = "Pietrus", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.C, Height = 194
                    },
                    new Player
                    {
                        Name = "Jakub", Surname = "Antosiewicz", Birthdate = new DateTime(1998, 01, 12),
                        Position = Postition.PG, Height = 180
                    },
                    new Player
                    {
                        Name = "Michał", Surname = "Sznajder", Birthdate = new DateTime(1998, 05, 06),
                        Position = Postition.SG, Height = 185
                    },
                    new Player
                    {
                        Name = "Radosław", Surname = "Lis", Birthdate = new DateTime(1998, 02, 12),
                        Position = Postition.SF, Height = 194
                    },
                    new Player
                    {
                        Name = "Mateusz", Surname = "Santarius", Birthdate = new DateTime(1998, 11, 11),
                        Position = Postition.PG, Height = 185
                    },
                    new Player
                    {
                        Name = "Mateusz", Surname = "Białek", Birthdate = new DateTime(1995, 11, 09),
                        Position = Postition.PF, Height = 188
                    },
                    new Player
                    {
                        Name = "Adam", Surname = "Bednarek", Birthdate = new DateTime(1985, 11, 09),
                        Position = Postition.PF, Height = 186
                    },
                    new Player
                    {
                        Name = "Krzysztof", Surname = "Adamczyk", Birthdate = new DateTime(1999, 11, 09),
                        Position = Postition.C, Height = 188
                    },
                    new Player
                    {
                        Name = "Kamil", Surname = "Surmiak", Birthdate = new DateTime(1996, 11, 09),
                        Position = Postition.PG, Height = 175
                    },
                    new Player
                    {
                        Name = "Anthony", Surname = "Davis", Birthdate = new DateTime(1995, 11, 09),
                        Position = Postition.C, Height = 205
                    },
                    new Player
                    {
                        Name = "Lebron", Surname = "James", Birthdate = new DateTime(1988, 11, 09),
                        Position = Postition.SF, Height = 203
                    },
                    new Player
                    {
                        Name = "Rajon", Surname = "Rondo", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.PG, Height = 183
                    },
                    new Player
                    {
                        Name = "Alex", Surname = "Caruso", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.SF, Height = 183
                    },
                    new Player
                    {
                        Name = "Damian", Surname = "Lillard", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.C, Height = 183
                    },
                    new Player
                    {
                        Name = "Robert", Surname = "Kubica", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.SG, Height = 183
                    },
                    new Player
                    {
                        Name = "Mike", Surname = "Malone", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.PG, Height = 183
                    },
                    new Player
                    {
                        Name = "Paweł", Surname = "Jumper", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.C, Height = 183
                    },
                    new Player
                    {
                        Name = "Michał", Surname = "Karmowski", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.PG, Height = 175
                    },
                    new Player
                    {
                        Name = "Maciej", Surname = "Musiał", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.SG, Height = 183
                    },
                    new Player
                    {
                        Name = "Aleksander", Surname = "Dziewa", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.C, Height = 187
                    },
                    new Player
                    {
                        Name = "Kamil", Surname = "Łączyński", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.PG, Height = 183
                    },
                    new Player
                    {
                        Name = "Dwight", Surname = "Howard", Birthdate = new DateTime(1998, 11, 09),
                        Position = Postition.C, Height = 206
                    },
                    new Player
                    {
                        Name = "Michael", Surname = "Jordan", Birthdate = new DateTime(1964, 11, 09),
                        Position = Postition.SG, Height = 183
                    },
                    new Player
                    {
                        Name = "Scotie", Surname = "Pippen", Birthdate = new DateTime(1970, 11, 13),
                        Position = Postition.SF, Height = 183
                    },
                    new Player
                    {
                        Name = "Robert", Surname = "Palczak", Birthdate = new DateTime(1969, 11, 09),
                        Position = Postition.C, Height = 201
                    },

                }; // 25 

                var divisions = new List<Division>
                {
                    new Division {Name = "Ekstraliga", Level = 1, ShortName = "EKSTRA"},
                    new Division {Name = "1 liga", Level = 2, ShortName = "1LIGA"},
                    new Division {Name = "2 liga", Level = 3, ShortName = "2LIGA"}
                }; // 3

                var seasons = new List<Season>
                {
                    new Season
                    {
                        Name = "2019", StartDate = new DateTime(2019, 01, 01), EndDate = new DateTime(2019, 12, 31)
                    },
                    new Season
                    {
                        Name = "2020", StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 12, 31)
                    },
                }; // 2

                var seasonDivisions = new List<SeasonDivision>
                {
                    new SeasonDivision {Division = divisions[0], Season = seasons[0]},
                    new SeasonDivision {Division = divisions[1], Season = seasons[0]},
                    new SeasonDivision {Division = divisions[2], Season = seasons[0]},
                    new SeasonDivision {Division = divisions[0], Season = seasons[1]},
                    new SeasonDivision {Division = divisions[1], Season = seasons[1]},
                    new SeasonDivision {Division = divisions[2], Season = seasons[1]}
                };  // 6

                var teams = new List<Team>
                {
                    new Team {Name = "Sto Twarzy Grzybiarzy", ShortName = "STG"},
                    new Team {Name = "Los Angeles Lakers", ShortName = "LAL"},
                    new Team {Name = "Portland Trail Blazers", ShortName = "PTB"},
                    new Team {Name = "Los Angeles Clippers", ShortName = "LAC"},
                    new Team {Name = "Denver Nuggets", ShortName = "DEN"},
                    new Team {Name = "HES Basketball Wrocław", ShortName = "HES"},
                    new Team {Name = "Strefa", ShortName = "STR"},
                }; // 7

                var teamSeasons = new List<TeamSeason>
                {
                    new TeamSeason {Team = teams[0], SeasonDivision =seasonDivisions[0]},
                    new TeamSeason {Team = teams[1], SeasonDivision =seasonDivisions[0]},
                    new TeamSeason {Team = teams[2], SeasonDivision =seasonDivisions[0]}
                }; // 3

                var playerSeasons = new List<PlayerSeason>
                {
                    new PlayerSeason{Player = players[0], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "1"},
                    new PlayerSeason{Player = players[1], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "2"},
                    new PlayerSeason{Player = players[2], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "3"},
                    new PlayerSeason{Player = players[3], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "4"},
                    new PlayerSeason{Player = players[4], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "5"},
                    new PlayerSeason{Player = players[5], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "6"},
                    new PlayerSeason{Player = players[6], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "7"},
                    new PlayerSeason{Player = players[7], SeasonDivision = seasonDivisions[0], Team = teamSeasons[0], JerseyNr = "8"},
                    new PlayerSeason{Player = players[8], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "9"},
                    new PlayerSeason{Player = players[9], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "10"},
                    new PlayerSeason{Player = players[10], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "11"},
                    new PlayerSeason{Player = players[11], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "12"},
                    new PlayerSeason{Player = players[12], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "13"},
                    new PlayerSeason{Player = players[13], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "14"},
                    new PlayerSeason{Player = players[14], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "15"},
                    new PlayerSeason{Player = players[15], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "16"},
                    new PlayerSeason{Player = players[16], SeasonDivision = seasonDivisions[0], Team = teamSeasons[1], JerseyNr = "17"},

                }; // 17

                var matches = new List<Match>
                {
                    new Match
                    {
                        StartDate = new DateTime(2020, 10, 01), Attendance = 200, SeasonDivision = seasonDivisions[0], TeamHome = new TeamMatch{Team = teams[0]}, TeamGuest = new TeamMatch{Team = teams[1]}, TeamSeasonGuest = teamSeasons[1],
                        TeamSeasonHome =  teamSeasons[0], Ended = false
                    },
                    new Match
                    {
                        StartDate = new DateTime(2020, 09, 16), Attendance = 400, SeasonDivision = seasonDivisions[0], TeamHome = new TeamMatch{Team = teams[1]}, TeamGuest = new TeamMatch{Team = teams[0]}, TeamSeasonGuest = teamSeasons[0],
                        TeamSeasonHome =  teamSeasons[1], Ended = false
                    }
                }; // 2 create team matches also


                await context.Player.AddRangeAsync(players);
                await context.Division.AddRangeAsync(divisions);
                await context.Season.AddRangeAsync(seasons);
                await context.SeasonDivision.AddRangeAsync(seasonDivisions);
                await context.PlayerSeason.AddRangeAsync(playerSeasons);
                await context.TeamSeason.AddRangeAsync(teamSeasons);
                await context.Team.AddRangeAsync(teams);
                await context.Match.AddRangeAsync(matches);

                await context.SaveChangesAsync();
            }
        }

    }
}
