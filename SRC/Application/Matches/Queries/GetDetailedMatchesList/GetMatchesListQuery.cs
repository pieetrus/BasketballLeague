using BasketballLeague.Application.Matches.Queries.GetMatchesList;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Matches.Queries.GetDetailedMatchesList
{
    public class GetDetailedMatchesListQuery : IRequest<IEnumerable<MatchListDto>>
    {
    }
}