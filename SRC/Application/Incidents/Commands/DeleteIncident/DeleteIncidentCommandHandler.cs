using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
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
            var entity = await _context.Incident.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Incident), request.Id);
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
