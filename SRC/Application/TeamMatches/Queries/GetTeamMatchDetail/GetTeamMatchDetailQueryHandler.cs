using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamMatches.Queries.GetTeamMatchDetail
{
    public class GetTeamMatchDetailQueryHandler : IRequestHandler<GetTeamMatchDetailQuery, TeamMatch>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTeamMatchDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }


        public async Task<TeamMatch> Handle(GetTeamMatchDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.TeamMatch
                .Include(x => x.Team)
                //.Include(x => x.Match)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TeamMatch), request.Id);
            }

            return entity;
        }
    }
}
