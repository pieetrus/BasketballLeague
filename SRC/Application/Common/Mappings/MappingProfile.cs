using AutoMapper;
using BasketballLeague.Application.Matches.Queries.GetMatchesList;
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

            CreateMap<Team, TeamPlayerList>();

            CreateMap<Match, MatchListDto>()
                .ForMember(x => x.TeamGuest, x => x.MapFrom(x => x.TeamGuest.Name))
                .ForMember(x => x.TeamHome, x => x.MapFrom(x => x.TeamHome.Name))
                .ForMember(x => x.Division, x => x.MapFrom(x => x.SeasonDivision.Division.Name));



        }
    }
}
