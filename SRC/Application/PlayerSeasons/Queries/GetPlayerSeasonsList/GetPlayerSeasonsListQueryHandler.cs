using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonsList
{
    public class GetPlayerSeasonsListQueryHandler : IRequestHandler<GetPlayerSeasonsListQuery, IEnumerable<PlayerSeasonListDto>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerSeasonsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerSeasonListDto>> Handle(GetPlayerSeasonsListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.PlayerSeason
                .Include(x => x.Player)
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Division)
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Season)
                .Include(x => x.Team).ThenInclude(x => x.Team)
                .AsQueryable();


            if (request.SeasonId != null && request.DivisionId != null)
            {
                queryable = queryable
                    .Where(x => x.SeasonDivision.SeasonId == request.SeasonId &&
                                x.SeasonDivision.DivisionId == request.DivisionId);

            }
            else if (request.SeasonId.HasValue)
            {
                queryable = queryable.Where(x => x.SeasonDivision.SeasonId == request.SeasonId);
            }

            if (request.TeamId != null)
            {
                queryable = queryable
                    .Where(x => x.TeamId == request.TeamId);

            }

            if (request.PlayerId != null)
            {
                queryable = queryable
                    .Where(x => x.PlayerId == request.PlayerId);

            }


            var playerSeasons = await queryable.OrderByDescending(x => x.Pts).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PlayerSeason>, IEnumerable<PlayerSeasonListDto>>(playerSeasons);
        }
    }
}