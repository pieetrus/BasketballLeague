using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Fouls.Queries.GetFoulsList
{
    public class GetFoulsListQueryHandler : IRequestHandler<GetFoulsListQuery, IEnumerable<Foul>>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetFoulsListQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Foul>> Handle(GetFoulsListQuery request, CancellationToken cancellationToken)
        {
            var fouls = await _context.Foul.ToListAsync(cancellationToken);

            return fouls;
        }
    }
}
