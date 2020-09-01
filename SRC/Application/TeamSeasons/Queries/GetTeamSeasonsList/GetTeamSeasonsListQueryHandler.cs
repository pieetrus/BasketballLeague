using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Application.Teams;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonsListQueryHandler : IRequestHandler<GetTeamSeasonsListQuery, IEnumerable<TeamDto>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamSeasonsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> Handle(GetTeamSeasonsListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.TeamSeason.Include(x => x.Team).AsQueryable();

            if (request.SeasonId != null && request.DivisionId != null)
            {
                queryable = queryable
                    .Where(x => x.SeasonDivision.SeasonId == request.SeasonId.Value &&
                                x.SeasonDivision.DivisionId == request.DivisionId.Value);

            }

            var teamSeasons = await queryable.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TeamSeason>, IEnumerable<TeamDto>>(teamSeasons).ToList();

        }
    }
}
