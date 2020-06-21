using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Coaches.Commands.DeleteCoach
{
    public class DeleteCoachCommandHandler : IRequestHandler<DeleteCoachCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteCoachCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }


        public async Task<Unit> Handle(DeleteCoachCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Coach.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Coach), request.Id);
            }

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
