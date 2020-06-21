using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamMatches.Queries.GetTeamMatchesList
{
    public class GetTeamMatchesListQuery : IRequest<IEnumerable<TeamMatch>>
    {
    }
}
