using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Matches.Queries.GetMatchesList
{
    public class GetMatchesListQuery : IRequest<IEnumerable<Match>>
    {
    }
}