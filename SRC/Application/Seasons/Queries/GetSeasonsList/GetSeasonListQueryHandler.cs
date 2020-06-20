using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Seasons.Queries.GetSeasonsList
{
    public class GetSeasonListQueryHandler : IRequestHandler<GetSeasonListQuery, IEnumerable<Season>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Season>> Handle(GetSeasonListQuery request, CancellationToken cancellationToken)
        {
            var seasons = await _context.Season.Include(x => x.SeasonDivisions).ThenInclude(x => x.Division).ToListAsync(cancellationToken);

            return seasons;
        }
    }
}
