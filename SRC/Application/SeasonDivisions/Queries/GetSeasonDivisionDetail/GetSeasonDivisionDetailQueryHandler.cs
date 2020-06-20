using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.SeasonDivisions.Queries.GetSeasonDivisionDetail
{
    public class GetSeasonDivisionDetailQueryHandler : IRequestHandler<GetSeasonDivisionDetailQuery, SeasonDivision>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetSeasonDivisionDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<SeasonDivision> Handle(GetSeasonDivisionDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SeasonDivision.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(SeasonDivision), request.Id);
            }

            return entity;
        }
    }
}