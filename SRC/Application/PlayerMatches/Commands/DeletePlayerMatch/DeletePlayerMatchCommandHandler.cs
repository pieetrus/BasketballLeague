using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerMatches.Commands.DeletePlayerMatch
{
    public class DeletePlayerMatchCommandHandler : IRequestHandler<DeletePlayerMatchCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeletePlayerMatchCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePlayerMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlayerMatch.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlayerMatch), request.Id);
            }

            _context.PlayerMatch.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
