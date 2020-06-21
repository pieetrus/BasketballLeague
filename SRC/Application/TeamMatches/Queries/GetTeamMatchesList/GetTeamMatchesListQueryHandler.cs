using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamMatches.Queries.GetTeamMatchesList
{
    public class GetTeamMatchesListQueryHandler : IRequestHandler<GetTeamMatchesListQuery, IEnumerable<TeamMatch>>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTeamMatchesListQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamMatch>> Handle(GetTeamMatchesListQuery request, CancellationToken cancellationToken)
        {
            var teamMatches = await _context.TeamMatch
                .ToListAsync(cancellationToken);

            return teamMatches;
        }
    }
}
