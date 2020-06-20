using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Referees.Commands.DeleteReferee
{
    public class DeleteRefereeCommandHandler : IRequestHandler<DeleteRefereeCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteRefereeCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRefereeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Referee
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Referee), request.Id);
            }

            _context.Referee.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}


