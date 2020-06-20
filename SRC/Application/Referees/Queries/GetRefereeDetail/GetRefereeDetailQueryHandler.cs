using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Referees.Queries.GetRefereeDetail
{
    public class GetRefereeDetailQueryHandler : IRequestHandler<GetRefereeDetailQuery, Referee>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetRefereeDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Referee> Handle(GetRefereeDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Referee.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Referee), request.Id);
            }

            return entity;
        }
    }
}