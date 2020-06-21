using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Substitutions.Queries.GetSubstitutionDetail
{
    public class GetSubstitutionDetailQuery : IRequest<Substitution>
    {
        public int Id { get; set; }
    }
}
