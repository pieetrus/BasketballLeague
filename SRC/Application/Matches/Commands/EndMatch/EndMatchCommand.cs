using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Commands.EndMatch
{
    public class EndMatchCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<EndMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EndMatchCommand request, CancellationToken cancellationToken)
            {
                var match = await _context.Match.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (match == null)
                    throw new NotFoundException(nameof(Match), request.Id);

                match.Ended = true;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
