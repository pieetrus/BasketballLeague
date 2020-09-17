using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Incidents.Commands.DeleteIncident
{
    public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteIncidentCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident
                .Include(x => x.Shot)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Incident), request.Id);
            }

            if (entity.IncidentType == IncidentType.SHOT)
            {
                var team = await _context.Match
                    .Include(x => x.TeamGuest)
                    .Include(x => x.TeamHome)
                    .FirstOrDefaultAsync(x => x.Id == entity.MatchId, cancellationToken);

                var teamMatch = entity.IsGuest ? team.TeamHome : team.TeamGuest;

                if (entity.Shot.Value == 2)
                {
                    teamMatch.Fg2a--;
                    if (entity.Shot.IsAccurate) teamMatch.Fg2m--;
                }
                else if (entity.Shot.Value == 3)
                {
                    teamMatch.Fg3a--;
                    if (entity.Shot.IsAccurate) teamMatch.Fg3m--; ;

                }
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
