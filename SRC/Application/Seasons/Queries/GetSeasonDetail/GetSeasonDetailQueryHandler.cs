using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Seasons.Queries.GetSeasonDetail
{
    public class GetSeasonDetailQueryHandler : IRequestHandler<GetSeasonDetailQuery, Season>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetSeasonDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Season> Handle(GetSeasonDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Season.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            return entity;
        }
    }
}