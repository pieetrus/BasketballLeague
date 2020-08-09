using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Substitutions.Commands.DeleteSubstitution
{
    public class DeleteSubstitutionCommandHandler : IRequestHandler<DeleteSubstitutionCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteSubstitutionCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSubstitutionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident.FirstOrDefaultAsync(x => x.Substitution.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Substitution), request.Id);
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}