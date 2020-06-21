using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Coaches.Queries.GetCoachDetail
{
    public class GetCoachDetailQueryHandler : IRequestHandler<GetCoachDetailQuery, Coach>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetCoachDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Coach> Handle(GetCoachDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Coach
                .Include(x => x.TeamSeasons)
                .FirstOrDefaultAsync(x => x.CoachId == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TeamMatch), request.Id);
            }

            return entity;
        }
    }
}
