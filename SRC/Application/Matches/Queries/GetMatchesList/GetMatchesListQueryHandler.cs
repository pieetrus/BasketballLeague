using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Queries.GetMatchesList
{
    public class GetMatchesListQueryHandler : IRequestHandler<GetMatchesListQuery, IEnumerable<Match>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetMatchesListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Match>> Handle(GetMatchesListQuery request, CancellationToken cancellationToken)
        {
            var matches = await _context.Match.ToListAsync(cancellationToken);

            return matches;
        }
    }
}