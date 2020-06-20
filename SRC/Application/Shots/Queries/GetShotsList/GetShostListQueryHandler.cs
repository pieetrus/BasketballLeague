using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Shots.Queries.GetShotsList
{
    public class GetShostListQueryHandler : IRequestHandler<GetShostListQuery, IEnumerable<Shot>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetShostListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Shot>> Handle(GetShostListQuery request, CancellationToken cancellationToken)
        {
            var matches = await _context.Shot.ToListAsync(cancellationToken);

            return matches;
        }
    }
}
