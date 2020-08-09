using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Timeout = BasketballLeague.Domain.Entities.Timeout;

namespace BasketballLeague.Application.Timeouts.Queries.GetTimeoutDetail
{
    public class GetTimeoutDetailQueryHandler : IRequestHandler<GetTimeoutDetailQuery, Timeout>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTimeoutDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Timeout> Handle(GetTimeoutDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Timeout
                .Include(x => x.Incident)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Timeout), request.Id);
            }

            return entity;
        }
    }
}
