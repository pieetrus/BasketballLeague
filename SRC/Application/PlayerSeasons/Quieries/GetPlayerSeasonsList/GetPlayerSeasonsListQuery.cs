using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.GetPlayerSeasons.Quieries.GetPlayerSeasonsList
{
    public class GetPlayerSeasonsListQuery : IRequest<IEnumerable<PlayerSeason>>
    {
    }
}
