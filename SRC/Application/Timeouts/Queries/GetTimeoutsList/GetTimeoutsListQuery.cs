using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Timeouts.Queries.GetTimeoutsList
{
    public class GetTimeoutsListQuery : IRequest<IEnumerable<Timeout>>
    {
    }
}
