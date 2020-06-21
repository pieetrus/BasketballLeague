using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Turnovers.Queries.GetTurnoverList
{
    public class GetTurnoversListQuery : IRequest<IEnumerable<Turnover>>
    {
    }
}
