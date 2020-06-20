using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchDetail
{
    public class GetPlayerMatchDetailQueryHandler : IRequestHandler<GetPlayerMatchDetailQuery, PlayerMatch>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetPlayerMatchDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<PlayerMatch> Handle(GetPlayerMatchDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlayerMatch
                .Include(x => x.Player)
                .Include(x => x.Match)
                .FirstOrDefaultAsync(x => x.PlayerMatchId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlayerMatch), request.Id);
            }

            return entity;
        }
    }
}