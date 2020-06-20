using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchesList
{
    public class GetPlayerMatchesListQueryHandler : IRequestHandler<GetPlayerMatchesListQuery, IEnumerable<PlayerMatch>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerMatchesListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerMatch>> Handle(GetPlayerMatchesListQuery request, CancellationToken cancellationToken)
        {
            var teams = await _context.PlayerMatch.ToListAsync(cancellationToken);

            return teams;
        }
    }
}