using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.FreeThrows.Queries.GetFreeThrowsList
{
    public class GetFreeThrowsListQuery : IRequest<IEnumerable<Domain.Entities.FreeThrows>>
    {
    }
}
