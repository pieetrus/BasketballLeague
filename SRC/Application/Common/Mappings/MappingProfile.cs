using AutoMapper;
using BasketballLeague.Application.aTest.MatchWithPlayerSeasonTest;
using BasketballLeague.Application.Divisions;
using BasketballLeague.Application.Matches.Queries.GetDetailedMatchesList;
using BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed;
using BasketballLeague.Application.PlayerMatches;
using BasketballLeague.Application.Players.Queries.GetPlayerDetail;
using BasketballLeague.Application.Players.Queries.GetPlayersList;
using BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonsList;
using BasketballLeague.Application.Seasons;
using BasketballLeague.Application.Teams;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using System.Linq;

namespace BasketballLeague.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerListDto>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.Teams, x => x.MapFrom(x => x.PlayerSeasons.Select(x => x.Team.Team)))
                .ForMember(x => x.Position, x => x.MapFrom(x => x.Position.GetDescription()));

            CreateMap<Player, GetPlayerDetailQueryHandler.PlayerDto>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.Position, x => x.MapFrom(x => x.Position.GetDescription()));

            CreateMap<PlayerSeason, PlayerSeasonListDto>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.TeamName, x => x.MapFrom(x => x.Team.Team.Name))
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Player.Name))
                .ForMember(x => x.Surname, x => x.MapFrom(x => x.Player.Surname))
                .ForMember(x => x.Height, x => x.MapFrom(x => x.Player.Height))
                .ForMember(x => x.Birthdate, x => x.MapFrom(x => x.Player.Birthdate))
                .ForMember(x => x.DivisionName, x => x.MapFrom(x => x.SeasonDivision.Division.Name))
                .ForMember(x => x.Season, x => x.MapFrom(x => x.SeasonDivision.Season.Name))
                .ForMember(x => x.Position, x => x.MapFrom(x => x.Player.Position.GetDescription()));

            CreateMap<PlayerSeason, PlayerBeforeMatchDto>()
                .ForMember(x => x.Id, x => x.MapFrom(playerSeason => playerSeason.Id))
                .ForMember(x => x.PlayerId, x => x.MapFrom(playerSeason => playerSeason.Player.Id))
                .ForMember(x => x.Name, x => x.MapFrom(playerSeason => playerSeason.Player.Name))
                .ForMember(x => x.Surname, x => x.MapFrom(playerSeason => playerSeason.Player.Surname))
                .ForMember(x => x.JerseyNr, x => x.MapFrom(playerSeason => playerSeason.JerseyNr))
                .ForMember(x => x.Position, x => x.MapFrom(playerSeason => playerSeason.Player.Position.GetDescription()))
                .ForMember(x => x.Height, x => x.MapFrom(playerSeason => playerSeason.Player.Height));

            CreateMap<Team, TeamDto>()
                .ForMember(x => x.LogoUrl, x => x.MapFrom(x => x.Logo.Url));

            CreateMap<TeamSeason, TeamDto>()
                .ForMember(x => x.LogoUrl, x => x.MapFrom(x => x.Team.Logo.Url))
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Team.Name))
                .ForMember(x => x.ShortName, x => x.MapFrom(x => x.Team.ShortName))
                ;


            CreateMap<Match, MatchListDto>()
                .ForMember(x => x.TeamGuest, x => x.MapFrom(x => x.TeamGuest.Team))
                .ForMember(x => x.TeamHome, x => x.MapFrom(x => x.TeamHome.Team))
                .ForMember(x => x.Division, x => x.MapFrom(x => x.SeasonDivision.Division.Name))
                .ForPath(x => x.TeamGuest.Fouls1Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls1Qtr))
                .ForPath(x => x.TeamGuest.Fouls2Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls2Qtr))
                .ForPath(x => x.TeamGuest.Fouls3Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls3Qtr))
                .ForPath(x => x.TeamGuest.Fouls4Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls4Qtr))
                .ForPath(x => x.TeamGuest.Timeouts1Half, x => x.MapFrom(x => x.TeamGuest.Timeouts1Half))
                .ForPath(x => x.TeamGuest.Timeouts2Half, x => x.MapFrom(x => x.TeamGuest.Timeouts2Half))
                .ForPath(x => x.TeamHome.Fouls1Qtr, x => x.MapFrom(x => x.TeamHome.Fouls1Qtr))
                .ForPath(x => x.TeamHome.Fouls2Qtr, x => x.MapFrom(x => x.TeamHome.Fouls2Qtr))
                .ForPath(x => x.TeamHome.Fouls3Qtr, x => x.MapFrom(x => x.TeamHome.Fouls3Qtr))
                .ForPath(x => x.TeamHome.Fouls4Qtr, x => x.MapFrom(x => x.TeamHome.Fouls4Qtr))
                .ForPath(x => x.TeamHome.Timeouts1Half, x => x.MapFrom(x => x.TeamHome.Timeouts1Half))
                .ForPath(x => x.TeamHome.Timeouts2Half, x => x.MapFrom(x => x.TeamHome.Timeouts2Half))
                .ForPath(x => x.TeamHome.LogoUrl, x => x.MapFrom(x => x.TeamHome.Team.Logo.Url))
                .ForPath(x => x.TeamGuest.LogoUrl, x => x.MapFrom(x => x.TeamGuest.Team.Logo.Url))
                .ForMember(x => x.LastIncidentMinutes, x => x.MapFrom(x => x.Incidents.LastOrDefault().Minutes))
                .ForMember(x => x.LastIncidentSeconds, x => x.MapFrom(x => x.Incidents.LastOrDefault().Seconds))
                .ForMember(x => x.LastIncidentQuater, x => x.MapFrom(x => x.Incidents.LastOrDefault().Quater))
                ;

            CreateMap<Match, MatchDetailedDto>()
                .ForMember(x => x.TeamGuest, x => x.MapFrom(x => x.TeamGuest.Team))
                .ForMember(x => x.TeamHome, x => x.MapFrom(x => x.TeamHome.Team))
                .ForMember(x => x.Division, x => x.MapFrom(x => x.SeasonDivision.Division.Name))
                .ForMember(x => x.TeamHomePlayers,
                    y => y.MapFrom(x =>
                        x.TeamSeasonHome.Players))
                .ForMember(x => x.TeamGuestPlayers,
                    y => y.MapFrom(x => x.TeamSeasonGuest.Players))
                .ForMember(x => x.TeamHomePts, x => x.MapFrom(x => x.TeamHome.Pts))
                .ForMember(x => x.TeamGuestPts, x => x.MapFrom(x => x.TeamGuest.Pts))
                .ForPath(x => x.TeamGuest.Fouls1Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls1Qtr))
                .ForPath(x => x.TeamGuest.Fouls2Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls2Qtr))
                .ForPath(x => x.TeamGuest.Fouls3Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls3Qtr))
                .ForPath(x => x.TeamGuest.Fouls4Qtr, x => x.MapFrom(x => x.TeamGuest.Fouls4Qtr))
                .ForPath(x => x.TeamGuest.Timeouts1Half, x => x.MapFrom(x => x.TeamGuest.Timeouts1Half))
                .ForPath(x => x.TeamGuest.Timeouts2Half, x => x.MapFrom(x => x.TeamGuest.Timeouts2Half))
                .ForPath(x => x.TeamHome.Fouls1Qtr, x => x.MapFrom(x => x.TeamHome.Fouls1Qtr))
                .ForPath(x => x.TeamHome.Fouls2Qtr, x => x.MapFrom(x => x.TeamHome.Fouls2Qtr))
                .ForPath(x => x.TeamHome.Fouls3Qtr, x => x.MapFrom(x => x.TeamHome.Fouls3Qtr))
                .ForPath(x => x.TeamHome.Fouls4Qtr, x => x.MapFrom(x => x.TeamHome.Fouls4Qtr))
                .ForPath(x => x.TeamHome.Timeouts1Half, x => x.MapFrom(x => x.TeamHome.Timeouts1Half))
                .ForPath(x => x.TeamHome.Timeouts2Half, x => x.MapFrom(x => x.TeamHome.Timeouts2Half))
                .ForMember(x => x.PlayersInGameIds, x => x.MapFrom(x => x.PlayerMatches.Select(x => x.PlayerSeasonId)))
                .ForMember(x => x.LastIncidentMinutes, x => x.MapFrom(x => x.Incidents.LastOrDefault().Minutes))
                .ForMember(x => x.LastIncidentSeconds, x => x.MapFrom(x => x.Incidents.LastOrDefault().Seconds))
                .ForMember(x => x.LastIncidentQuater, x => x.MapFrom(x => x.Incidents.LastOrDefault().Quater))
                ;

            CreateMap<Season, SeasonDto>()
                .ForMember(x => x.Divisions, x => x.MapFrom(x => x.SeasonDivisions.Select(x => x.Division)));

            CreateMap<Division, DivisionDto>();

            CreateMap<Match, MatchTestDto>()
                .ForMember(x => x.TeamGuest, x => x.MapFrom(x => x.TeamGuest.Team.Name))
                .ForMember(x => x.TeamHome, x => x.MapFrom(x => x.TeamHome.Team.Name))
                .ForMember(x => x.TeamSeasonGuest, x => x.MapFrom(x => x.TeamSeasonGuest.Team.Name))
                .ForMember(x => x.TeamSeasonHome, x => x.MapFrom(x => x.TeamSeasonHome.Team.Name));

            CreateMap<PlayerMatch, PlayerMatchDto>()
                .ForMember(x => x.Player, x => x.MapFrom(x => x.PlayerSeason));

        }
    }
}
