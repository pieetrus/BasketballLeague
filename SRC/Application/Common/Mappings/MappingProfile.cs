﻿using AutoMapper;
using BasketballLeague.Application.Matches.Queries.GetDetailedMatchesList;
using BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed;
using BasketballLeague.Application.PlayerMatches;
using BasketballLeague.Application.Players.Queries.GetPlayersList;
using BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonsList;
using BasketballLeague.Application.Teams;
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
                .ForMember(x => x.Position, x => x.MapFrom(x => x.Position.ToString()));

            CreateMap<PlayerSeason, PlayerSeasonListDto>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.TeamName, x => x.MapFrom(x => x.Team.Team.Name))
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Player.Name))
                .ForMember(x => x.DivisionName, x => x.MapFrom(x => x.SeasonDivision.Division.Name))
                .ForMember(x => x.Position, x => x.MapFrom(x => x.Player.Position.ToString()));

            CreateMap<PlayerSeason, PlayerBeforeMatchDto>()
                .ForMember(x => x.Id, x => x.MapFrom(playerMatch => playerMatch.Id))
                .ForMember(x => x.Name, x => x.MapFrom(playerMatch => playerMatch.Player.Name))
                .ForMember(x => x.Surname, x => x.MapFrom(playerMatch => playerMatch.Player.Surname))
                .ForMember(x => x.JerseyNr, x => x.MapFrom(playerMatch => playerMatch.JerseyNr))
                .ForMember(x => x.Position, x => x.MapFrom(playerMatch => playerMatch.Player.Position));

            CreateMap<Team, TeamPlayerList>();

            CreateMap<Match, MatchListDto>()
                .ForMember(x => x.TeamGuest, x => x.MapFrom(x => x.TeamGuest.Team.Name))
                .ForMember(x => x.TeamHome, x => x.MapFrom(x => x.TeamHome.Team.Name))
                .ForMember(x => x.Division, x => x.MapFrom(x => x.SeasonDivision.Division.Name));

            CreateMap<Match, MatchDetailedDto>()
                .ForMember(x => x.TeamGuest, x => x.MapFrom(x => x.TeamGuest.Team.Name))
                .ForMember(x => x.TeamHome, x => x.MapFrom(x => x.TeamHome.Team.Name))
                .ForMember(x => x.Division, x => x.MapFrom(x => x.SeasonDivision.Division.Name))
                .ForMember(x => x.TeamHomePlayers,
                    y => y.MapFrom(x =>
                        x.TeamSeasonHome.Players))
                .ForMember(x => x.TeamGuestPlayers,
                    y => y.MapFrom(x => x.TeamSeasonGuest.Players));


        }
    }
}
