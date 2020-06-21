using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Fouls.Queries.GetFoulsList
{
    public class GetFoulsListQuery : IRequest<IEnumerable<Foul>>
    {
    }
}
