using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Teams.Queries.GetTeamDetail
{
    public class GetTeamDetailQueryHandler : IRequestHandler<GetTeamDetailQuery, Team>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetTeamDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Team> Handle(GetTeamDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Team.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            return entity;
        }
    }
}
