using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Players.Queries
{
    public class GetPlayersListQuery : IRequest<IEnumerable<Player>>
    {

    }
}
