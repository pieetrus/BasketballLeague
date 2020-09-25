using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed
{
    public class GetMatchDetailDetailedQueryHandler : IRequestHandler<GetMatchDetailDetailedQuery, MatchDetailedDto>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetMatchDetailDetailedQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchDetailedDto> Handle(GetMatchDetailDetailedQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Match
                .Include(x => x.TeamHome).ThenInclude(x => x.Team).ThenInclude(x => x.Logo)
                .Include(x => x.TeamGuest).ThenInclude(x => x.Team).ThenInclude(x => x.Logo)
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Division)
                .Include(x => x.TeamSeasonHome).ThenInclude(x => x.Players).ThenInclude(x => x.Player)
                .Include(x => x.TeamSeasonGuest).ThenInclude(x => x.Players).ThenInclude(x => x.Player)
                .Include(x => x.PlayerMatches)
                .Include(x => x.Incidents)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Match), request.Id);

            return _mapper.Map<Match, MatchDetailedDto>(entity);
        }
    }
}
