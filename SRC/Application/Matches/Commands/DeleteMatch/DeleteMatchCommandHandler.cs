using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using MediatR;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Commands.DeleteMatch
{
    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteMatchCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Match
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            _context.Match.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
