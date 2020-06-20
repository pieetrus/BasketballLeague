using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionDetail
{
    public class GetDivisionDetailQueryHandler : IRequestHandler<GetDivisionDetailQuery, Division>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetDivisionDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Division> Handle(GetDivisionDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Division.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Division), request.Id);
            }

            return entity;
        }
    }
}
