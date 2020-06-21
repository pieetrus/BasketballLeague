using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonDetail
{
    public class GetTeamSeasonDetailQueryHandler : IRequestHandler<GetTeamSeasonDetailQuery, TeamSeason>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTeamSeasonDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<TeamSeason> Handle(GetTeamSeasonDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.TeamSeason
                .Include(x => x.Team)
                .FirstOrDefaultAsync(x => x.TeamSeasonId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TeamSeason), request.Id);
            }

            return entity;
        }
    }
}
