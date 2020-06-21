using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerSeasons.Commands.DeletePlayerSeason
{
    public class DeletePlayerSeasonCommandHandler : IRequestHandler<DeletePlayerSeasonCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeletePlayerSeasonCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePlayerSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlayerSeason.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlayerSeason), request.Id);
            }

            _context.PlayerSeason.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
