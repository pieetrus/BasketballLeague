using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchesList
{
    public class GetPlayerMatchesListQueryHandler : IRequestHandler<GetPlayerMatchesListQuery, IEnumerable<PlayerMatchDto>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerMatchesListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerMatchDto>> Handle(GetPlayerMatchesListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.PlayerMatch
                .Include(x => x.PlayerSeason).ThenInclude(x => x.Player)
                .AsQueryable();

            if (request.MatchId != null)
            {
                queryable = queryable.Where(x => x.MatchId == request.MatchId);
            }

            var players = await queryable.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PlayerMatch>, IEnumerable<PlayerMatchDto>>(players).ToList();
        }
    }
}