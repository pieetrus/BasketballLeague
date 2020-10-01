using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonListQueryHandler : IRequestHandler<GetTeamSeasonListQuery, IEnumerable<TeamSeason>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamSeasonListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamSeason>> Handle(GetTeamSeasonListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.TeamSeason.Include(x => x.Team).AsQueryable();

            var teamSeasons = await queryable.OrderByDescending(x => x.Pts).ToListAsync(cancellationToken);

            return teamSeasons;

        }
    }
}
