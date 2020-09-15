using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Application.PlayerMatches;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.StatsHelpers.Queries.GetPlayerBeforeMatchByPlayerSeasonList
{
    public class GetPlayerBeforeMatchByPlayerSeasonListQueryHandler : IRequestHandler<GetPlayerBeforeMatchByPlayerSeasonListQuery, IEnumerable<PlayerBeforeMatchDto>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerBeforeMatchByPlayerSeasonListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerBeforeMatchDto>> Handle(GetPlayerBeforeMatchByPlayerSeasonListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlayerSeason
                .Where(x => request.Id.Contains(x.Id))
                .Include(x => x.Player)
                .ToListAsync(cancellationToken);

            if (entity.Count() != request.Id.Count())
            {
                var diff = request.Id.Except(entity.Select(x => x.Id)).ToList();
                throw new NotFoundException(nameof(PlayerSeason), diff[0]);
            }

            return _mapper.Map<IEnumerable<PlayerSeason>, IEnumerable<PlayerBeforeMatchDto>>(entity);
        }
    }
}
