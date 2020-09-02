using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamSeasons.Commands.DeleteTeamSeason
{
    public class DeleteTeamSeasonCommandHandler : IRequestHandler<DeleteTeamSeasonCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteTeamSeasonCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeamSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TeamSeason.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TeamSeason), request.Id);
            }

            _context.TeamSeason.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
