using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Queries.GetMatchDetail
{
    public class GetMatchDetailQueryHandler : IRequestHandler<GetMatchDetailQuery, Match>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetMatchDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Match> Handle(GetMatchDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Match
                //.Include(x => x.PlayerMatches)
                //.Include(x => x.TeamMatches)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Match), request.Id);

            return entity;
        }
    }
}