using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.SeasonDivisions.Queries.GetSeasonDivisionsList
{
    public class GetSeasonDivisionsListQueryHandler : IRequestHandler<GetSeasonDivisionsListQuery, IEnumerable<SeasonDivision>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonDivisionsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SeasonDivision>> Handle(GetSeasonDivisionsListQuery request, CancellationToken cancellationToken)
        {
            var seasonDivisions = await _context.SeasonDivision.Include(x => x.Season).ToListAsync(cancellationToken);

            return seasonDivisions;
        }
    }
}


