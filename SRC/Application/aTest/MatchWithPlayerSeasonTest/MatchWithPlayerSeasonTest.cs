using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.aTest.MatchWithPlayerSeasonTest
{
    public class MatchWithPlayerSeasonTestQuery : IRequest<IEnumerable<MatchTestDto>>
    {
    }

    public class MatchWithPlayerSeasonTestQueryHandler : IRequestHandler<MatchWithPlayerSeasonTestQuery, IEnumerable<MatchTestDto>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public MatchWithPlayerSeasonTestQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatchTestDto>> Handle(MatchWithPlayerSeasonTestQuery request, CancellationToken cancellationToken)
        {
            var matches = await _context.Match
                .Include(x => x.SeasonDivision).ThenInclude(x => x.Division)
                .Include(x => x.TeamHome).ThenInclude(x => x.Team)
                .Include(x => x.TeamGuest).ThenInclude(x => x.Team)
                .Include(x => x.TeamSeasonHome).ThenInclude(x => x.Team)
                .Include(x => x.TeamSeasonGuest).ThenInclude(x => x.Team)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Match>, IEnumerable<MatchTestDto>>(matches).ToList();
        }
    }

    public class MatchTestDto
    {
        public int Id { get; set; }
        public virtual string TeamGuest { get; set; }
        public virtual string TeamHome { get; set; }
        public virtual string TeamSeasonGuest { get; set; }
        public virtual string TeamSeasonHome { get; set; }
    }

}
