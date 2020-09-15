using BasketballLeague.Application.PlayerMatches;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.StatsHelpers.Queries.GetPlayerBeforeMatchByPlayerSeasonList
{
    public class GetPlayerBeforeMatchByPlayerSeasonListQuery : IRequest<IEnumerable<PlayerBeforeMatchDto>>
    {
        public IEnumerable<int> Id { get; set; }
    }
}
