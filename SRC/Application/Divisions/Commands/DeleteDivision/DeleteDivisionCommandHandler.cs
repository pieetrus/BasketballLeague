using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Commands.DeleteDivision
{

    public class DeleteDivisionCommandHandler : IRequestHandler<DeleteDivisionCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteDivisionCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDivisionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Division
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Division), request.Id);
            }

            _context.Division.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
