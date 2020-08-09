using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Timeout = BasketballLeague.Domain.Entities.Timeout;

namespace BasketballLeague.Application.Timeouts.Commands.DeleteTimeout
{
    public class DeleteTimeoutCommandHandler : IRequestHandler<DeleteTimeoutCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteTimeoutCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTimeoutCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident.FirstOrDefaultAsync(x => x.Timeout.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Timeout), request.Id);
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}