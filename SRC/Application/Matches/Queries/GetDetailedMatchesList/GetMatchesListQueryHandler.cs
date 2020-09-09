using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Queries.GetDetailedMatchesList
{
    public class GetDetailedMatchesListQueryHandler : IRequestHandler<GetDetailedMatchesListQuery, IEnumerable<MatchListDto>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailedMatchesListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatchListDto>> Handle(GetDetailedMatchesListQuery request, CancellationToken cancellationToken)
        {
            var matches = await _context.Match
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Division)
                .Include(x => x.TeamHome).ThenInclude(x => x.Team)
                .Include(x => x.TeamGuest).ThenInclude(x => x.Team)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Match>, IEnumerable<MatchListDto>>(matches).ToList();
        }
    }
}