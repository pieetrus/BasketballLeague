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
    public class GetTeamSeasonListQueryHandler : IRequestHandler<GetTeamSeasonListQuery, IEnumerable<TeamSeasonListDto>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamSeasonListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamSeasonListDto>> Handle(GetTeamSeasonListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.TeamSeason
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Season)
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Division)
                .Include(x => x.Team)
                .AsQueryable();

            if (request.TeamId.HasValue)
            {
                queryable = queryable.Where(x => x.TeamId == request.TeamId);
            }

            var teamSeasons = await queryable.OrderByDescending(x => x.Pts).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TeamSeason>, IEnumerable<TeamSeasonListDto>>(teamSeasons);
        }
    }
}
