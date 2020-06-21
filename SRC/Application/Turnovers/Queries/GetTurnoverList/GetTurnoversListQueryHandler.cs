using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Queries.GetTurnoverList
{
    public class GetTurnoversListQueryHandler : IRequestHandler<GetTurnoversListQuery, IEnumerable<Turnover>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTurnoversListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Turnover>> Handle(GetTurnoversListQuery request, CancellationToken cancellationToken)
        {
            var turnovers = await _context.Turnover.ToListAsync(cancellationToken);

            return turnovers;
        }
    }
}