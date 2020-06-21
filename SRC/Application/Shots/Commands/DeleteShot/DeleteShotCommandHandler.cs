using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Shots.Commands.DeleteShot
{
    public class DeleteShotCommandHandler : IRequestHandler<DeleteShotCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteShotCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteShotCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Shot
                //.Include(x => x.Incident)
                .FirstOrDefaultAsync(x => x.ShotId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            _context.Shot.Remove(entity);
            //_context.Incident.Remove(entity.Incident);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");

        }
    }
}
