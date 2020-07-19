﻿using AutoMapper;
using BasketballLeague.Application.Players.Queries.GetPlayersList;
using BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonsList;
using BasketballLeague.Domain.Entities;

namespace BasketballLeague.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerListDto>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.PlayerId))
                .ForMember(x => x.Position, x=> x.MapFrom(x => x.Position.ToString()));

            CreateMap<PlayerSeason, PlayerSeasonListDto>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.PlayerSeasonId))
                .ForMember(x => x.TeamName, x => x.MapFrom(x => x.Team.Team.Name))
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Player.Name))
                .ForMember(x => x.DivisionName, x => x.MapFrom(x => x.SeasonDivision.Division.Name))
                .ForMember(x => x.Position, x => x.MapFrom(x => x.Player.Position.ToString()));
        }
    }
}
