using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var playerSeasons = await _context.PlayerSeason
                .Include(x => x.Player)
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Division)
                .Include(x => x.Team).ThenInclude(x => x.Team)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PlayerSeason>, IEnumerable<PlayerSeasonListDto>>(playerSeasons);
        }
    }
}