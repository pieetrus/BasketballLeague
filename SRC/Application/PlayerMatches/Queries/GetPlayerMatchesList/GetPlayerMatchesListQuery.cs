using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchesList
{
    public class GetPlayerMatchesListQuery : IRequest<IEnumerable<PlayerMatch>>
    {
    }
}
