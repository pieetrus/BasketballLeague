using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.JumpBalls.Commands.DeleteJumpBall
{
    public class DeleteJumpBallCommandHandler : IRequestHandler<DeleteJumpBallCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteJumpBallCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteJumpBallCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident.FirstOrDefaultAsync(x => x.JumpBall.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JumpBall), request.Id);
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}