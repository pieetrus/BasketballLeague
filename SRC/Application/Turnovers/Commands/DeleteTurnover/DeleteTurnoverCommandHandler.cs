using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Commands.DeleteTurnover
{
    public class DeleteTurnoverCommandHandler : IRequestHandler<DeleteTurnoverCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteTurnoverCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTurnoverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident.FirstOrDefaultAsync(x => x.Turnover.TurnoverId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Turnover), request.Id);
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}