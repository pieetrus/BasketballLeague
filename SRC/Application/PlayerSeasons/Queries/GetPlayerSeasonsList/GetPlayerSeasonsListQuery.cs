using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonsList
{
    public class GetPlayerSeasonsListQuery : IRequest<IEnumerable<PlayerSeasonListDto>>
    {
    }
}
