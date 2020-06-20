using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.SeasonDivisions.Commands.DeleteSeasonDivision
{
    public class DeleteSeasonDivisionCommandHandler : IRequestHandler<DeleteSeasonDivisionCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteSeasonDivisionCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSeasonDivisionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Season
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Season), request.Id);
            }

            _context.Season.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}


