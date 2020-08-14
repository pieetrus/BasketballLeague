using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionsList
{
    public class GetDivisionsListQuery : IRequest<IEnumerable<DivisionDto>>
    {
    }
}