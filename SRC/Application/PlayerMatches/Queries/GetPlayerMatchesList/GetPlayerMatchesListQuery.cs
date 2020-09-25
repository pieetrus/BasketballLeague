using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchesList
{
    public class GetPlayerMatchesListQuery : IRequest<IEnumerable<PlayerMatchDto>>
    {
        public int? MatchId { get; set; }

        public GetPlayerMatchesListQuery(int? matchId)
        {
            MatchId = matchId;
        }
    }
}
