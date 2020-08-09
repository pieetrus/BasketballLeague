using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Shots.Queries.GetShotDetail
{
    public class GetShotDetailQueryHandler : IRequestHandler<GetShotDetailQuery, Shot>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetShotDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Shot> Handle(GetShotDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Shot
                .Include(x => x.Incident)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Shot), request.Id);
            }

            return entity;
        }
    }
}