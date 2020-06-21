using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Substitutions.Queries.GetSubstitutionsList
{
    public class GetSubstitutionsListQuery : IRequest<IEnumerable<Substitution>>
    {
    }
}
