using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Commands.CreateTurnover
{
    public class CreateTurnoverCommand : IRequest<int>
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }
        public bool IsGuest { get; set; }

        public int PlayerId { get; set; }
        public int? PlayerStealId { get; set; }

        public TurnoverType TurnoverType { get; set; }



        public class Handler : IRequestHandler<CreateTurnoverCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateTurnoverCommand request, CancellationToken cancellationToken)
            {
                var playerMatchTurnover = await _context.PlayerMatch
                    .Include(x => x.PlayerSeason)
                    .FirstOrDefaultAsync(x => x.PlayerSeason.PlayerId == request.PlayerId && x.MatchId == request.MatchId, cancellationToken);

                Match match = await _context.Match
                    .Include(x => x.TeamGuest)
                    .Include(x => x.TeamHome)
                    .FirstOrDefaultAsync(x => x.Id == request.MatchId, cancellationToken);
                TeamMatch teamMatchWhoTurnover;
                TeamMatch teamMatchWhoSteal;
                if (request.IsGuest)
                {
                    teamMatchWhoTurnover = match.TeamGuest;
                    teamMatchWhoSteal = match.TeamHome;
                }
                else
                {
                    teamMatchWhoTurnover = match.TeamHome;
                    teamMatchWhoSteal = match.TeamGuest;
                }

                if (playerMatchTurnover == null)
                {
                    var playerSeason =
                        await _context.PlayerSeason.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerId,
                            cancellationToken);

                    playerMatchTurnover = new PlayerMatch { PlayerSeasonId = playerSeason.Id, MatchId = request.MatchId };

                    _context.PlayerMatch.Add(playerMatchTurnover);
                }

                playerMatchTurnover.Tov++;
                teamMatchWhoTurnover.Tov++;

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.TURNOVER,
                    Quater = request.Quater,
                    Flagged = request.Flagged,
                    IsGuest = request.IsGuest
                };

                var turnover = new Turnover
                {
                    PlayerId = request.PlayerId,
                    TurnoverType = request.TurnoverType,
                    Incident = incident
                };

                if (request.PlayerStealId.HasValue)
                {
                    var playerMatchSteal = await _context.PlayerMatch
                        .Include(x => x.PlayerSeason)
                        .FirstOrDefaultAsync(x => x.PlayerSeason.PlayerId == request.PlayerStealId && x.MatchId == request.MatchId, cancellationToken);

                    if (playerMatchSteal == null)
                    {
                        var playerSeason =
                            await _context.PlayerSeason.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerStealId,
                                cancellationToken);

                        playerMatchSteal = new PlayerMatch { PlayerSeasonId = playerSeason.Id, MatchId = request.MatchId };

                        _context.PlayerMatch.Add(playerMatchSteal);
                    }

                    playerMatchSteal.Stl++;
                    teamMatchWhoSteal.Stl++;

                    var steal = new Steal { PlayerId = request.PlayerStealId.Value, Turnover = turnover };

                    _context.Steal.Add(steal);
                }

                _context.Turnover.Add(turnover);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    return incident.Id;
                }

                throw new Exception("Error saving changes");
            }
        }
    }
}
