using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.FreeThrows.Queries.GetFreeThrowDetail
{
    public class GetFreeThrowDetailQueryHandler : IRequestHandler<GetFreeThrowDetailQuery, Domain.Entities.FreeThrows>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetFreeThrowDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Entities.FreeThrows> Handle(GetFreeThrowDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.FreeThrow
                .Include(x => x.PlayerShooter)
                .Include(x => x.Foul)
                .ThenInclude(x => x.Incident)
                .FirstOrDefaultAsync(x => x.FoulId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.FreeThrows), request.Id);
            }

            return entity;
        }
    }
}