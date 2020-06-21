using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Coaches.Queries.GetCoachesList
{
    public class GetCoachesListQueryHandler : IRequestHandler<GetCoachesListQuery, IEnumerable<Coach>>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetCoachesListQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coach>> Handle(GetCoachesListQuery request, CancellationToken cancellationToken)
        {
            var coaches = await _context.Coach
                .ToListAsync(cancellationToken);

            return coaches;
        }
    }
}