using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonDetail
{
    public class GetPlayerSeasonDetailQueryHandler : IRequestHandler<GetPlayerSeasonDetailQuery, PlayerSeason>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetPlayerSeasonDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<PlayerSeason> Handle(GetPlayerSeasonDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlayerSeason
                .Include(x => x.Player)
                .Include(x => x.SeasonDivision)
                .FirstOrDefaultAsync(x => x.PlayerSeasonId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlayerSeason), request.Id);
            }

            return entity;
        }
    }
}