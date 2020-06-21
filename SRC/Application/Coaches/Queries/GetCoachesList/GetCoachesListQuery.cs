using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Coaches.Queries.GetCoachesList
{
    public class GetCoachesListQuery : IRequest<IEnumerable<Coach>>
    {
    }
}
