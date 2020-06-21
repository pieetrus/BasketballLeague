using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamMatches.Commands.DeleteTeamMatch
{
    public class DeleteTeamMatchCommandHandler : IRequestHandler<DeleteTeamMatchCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteTeamMatchCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeamMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TeamMatch.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TeamMatch), request.Id);
            }

            _context.TeamMatch.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
