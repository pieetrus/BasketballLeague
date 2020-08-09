

using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Queries.GetTurnoverDetail
{
    public class GetTurnoversListQueryHandler : IRequestHandler<GetTurnoverDetailQuery, Turnover>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTurnoversListQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Turnover> Handle(GetTurnoverDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Turnover
                .Include(x => x.Incident)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Turnover), request.Id);
            }

            return entity;
        }
    }
}