using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Incidents.Queries.GetIncidentDetail
{
    public class GetIncidentDetailQueryHandler : IRequestHandler<GetIncidentDetailQuery, Incident>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetIncidentDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Incident> Handle(GetIncidentDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident
                .Include(x => x.Shot).ThenInclude(x => x.Rebound)
                .Include(x => x.Shot).ThenInclude(x => x.Assist)
                .Include(x => x.Substitution)
                .Include(x => x.Timeout)
                .Include(x => x.Foul)
                .Include(x => x.Turnover)
                .Include(x => x.Match)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Incident), request.Id);
            }

            return entity;
        }
    }
}
