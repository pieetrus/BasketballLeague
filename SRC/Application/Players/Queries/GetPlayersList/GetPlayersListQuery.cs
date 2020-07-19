using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Players.Queries.GetPlayersList
{
    public class GetPlayersListQuery : IRequest<IEnumerable<PlayerListDto>>
    {

    }
}
