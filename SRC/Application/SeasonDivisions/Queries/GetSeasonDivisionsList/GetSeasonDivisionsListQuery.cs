using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.SeasonDivisions.Queries.GetSeasonDivisionsList
{
    public class GetSeasonDivisionsListQuery : IRequest<IEnumerable<SeasonDivision>>
    {
    }
}
