using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeout = BasketballLeague.Domain.Entities.Timeout;

namespace BasketballLeague.Application.Timeouts.Queries.GetTimeoutsList
{
    public class GetTimeoutsListQueryHandler : IRequestHandler<GetTimeoutsListQuery, IEnumerable<Timeout>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTimeoutsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Timeout>> Handle(GetTimeoutsListQuery request, CancellationToken cancellationToken)
        {
            var timeouts = await _context.Timeout.ToListAsync(cancellationToken);

            return timeouts;
        }
    }
}