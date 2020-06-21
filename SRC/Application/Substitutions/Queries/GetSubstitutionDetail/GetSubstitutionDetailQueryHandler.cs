using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Substitutions.Queries.GetSubstitutionDetail
{
    public class GetSubstitutionDetailQueryHandler : IRequestHandler<GetSubstitutionDetailQuery, Substitution>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetSubstitutionDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Substitution> Handle(GetSubstitutionDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Substitution
                .Include(x => x.Incident)
                .FirstOrDefaultAsync(x => x.SubstitutionId == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Substitution), request.Id);
            }

            return entity;
        }
    }
}