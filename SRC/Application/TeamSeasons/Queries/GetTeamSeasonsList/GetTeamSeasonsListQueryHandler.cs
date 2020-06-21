using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonsListQueryHandler : IRequestHandler<GetTeamSeasonsListQuery, IEnumerable<TeamSeason>>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTeamSeasonsListQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamSeason>> Handle(GetTeamSeasonsListQuery request, CancellationToken cancellationToken)
        {
            var teamSeasons = await _context.TeamSeason.ToListAsync();

            return teamSeasons;
        }
    }
}
