using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.FreeThrows.Commands.DeleteFreeThrow
{
    public class DeleteFreeThrowCommandHandler : IRequestHandler<DeleteFreeThrowCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteFreeThrowCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFreeThrowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FreeThrow.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JumpBall), request.Id);
            }

            _context.FreeThrow.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}