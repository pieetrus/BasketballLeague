using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Referees.Queries.GetRefereesList
{
    public class GetRefereesListQuery : IRequest<IEnumerable<Referee>>
    {
    }
    
}
