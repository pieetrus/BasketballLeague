using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Fouls.Queries.GetFoulDetail
{
    public class GetFoulDetailQueryHandler : IRequestHandler<GetFoulDetailQuery, Foul>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetFoulDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Foul> Handle(GetFoulDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Foul
                .Include(x => x.PlayerWhoFouled)
                .Include(x => x.PlayerWhoWasFouled)
                .Include(x => x.Incident)
                .ThenInclude(x => x.Match)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Foul), request.Id);
            }

            return entity;
        }
    }
}