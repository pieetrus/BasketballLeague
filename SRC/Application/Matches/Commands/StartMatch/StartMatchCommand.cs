using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Commands.StartMatch
{
    public class StartMatchCommand : IRequest
    {
        public int Id { get; set; }
        public string TeamHomeJerseyColor { get; set; }
        public string TeamGuestJerseyColor { get; set; }
        public List<int> TeamHomePlayerSeasonIds { get; set; }
        public List<int> TeamGuestPlayerSeasonIds { get; set; }

        public class Handler : IRequestHandler<StartMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(StartMatchCommand request, CancellationToken cancellationToken)
            {
                var match = await _context.Match
                    .Include(x => x.PlayerMatches)
                    .Include(x => x.Incidents)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (match == null)
                    throw new NotFoundException(nameof(Match), request.Id);

                match.Started = true;

                match.TeamHomeJerseyColor = request.TeamHomeJerseyColor;
                match.TeamGuestJerseyColor = request.TeamGuestJerseyColor;

                var playerMatchList = new List<PlayerMatch>();

                foreach (var playerSeasonId in request.TeamHomePlayerSeasonIds)
                {
                    var playerMatch = new PlayerMatch { PlayerSeasonId = playerSeasonId, MatchId = request.Id, IsGuest = false };
                    playerMatchList.Add(playerMatch);
                }

                foreach (var playerSeasonId in request.TeamGuestPlayerSeasonIds)
                {
                    var playerMatch = new PlayerMatch { PlayerSeasonId = playerSeasonId, MatchId = request.Id, IsGuest = true };
                    playerMatchList.Add(playerMatch);
                }

                _context.PlayerMatch.AddRange(playerMatchList);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
