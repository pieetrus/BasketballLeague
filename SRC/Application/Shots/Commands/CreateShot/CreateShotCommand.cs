using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Shots.Commands.CreateShot
{
    public class CreateShotCommand : IRequest<int>
    {
        public int MatchId { get; set; }
        public string Seconds { get; set; }
        public string Minutes { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }
        public int PlayerId { get; set; }
        public ShotType ShotType { get; set; }
        public bool IsAccurate { get; set; }
        public bool IsFastAttack { get; set; }
        public int Value { get; set; }
        public int? PlayerAssistId { get; set; }
        public bool IsGuest { get; set; }

        public ReboundType? ReboundType { get; set; }
        public int? PlayerReboundId { get; set; }
        public int? TeamReboundId { get; set; }





        public class Handler : IRequestHandler<CreateShotCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateShotCommand request, CancellationToken cancellationToken)
            {
                Match match;
                TeamMatch teamMatch;
                if (request.IsGuest)
                {
                    match = await _context.Match.Include(x => x.TeamGuest)
                        .FirstOrDefaultAsync(x => x.Id == request.MatchId, cancellationToken);
                    teamMatch = match.TeamGuest;
                }
                else
                {
                    match = await _context.Match.Include(x => x.TeamHome)
                        .FirstOrDefaultAsync(x => x.Id == request.MatchId, cancellationToken);
                    teamMatch = match.TeamHome;
                }

                if (match == null)
                    throw new NotFoundException(nameof(Match), request.MatchId);
                if (teamMatch == null)
                    throw new NotFoundException(nameof(TeamMatch), request.MatchId);

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Seconds = request.Seconds,
                    Minutes = request.Minutes,
                    IncidentType = IncidentType.SHOT,
                    Quater = request.Quater,
                    Flagged = request.Flagged,
                    IsGuest = request.IsGuest
                };

                var shot = new Shot
                {
                    PlayerId = request.PlayerId,
                    ShotType = request.ShotType,
                    IsFastAttack = request.IsFastAttack,
                    IsAccurate = request.IsAccurate,
                    Value = request.Value,
                    Incident = incident,
                };

                if (shot.Value == 2)
                {
                    teamMatch.Fg2a++;
                    if (shot.IsAccurate) teamMatch.Fg2m++;
                }
                else if (shot.Value == 3)
                {
                    teamMatch.Fg3a++;
                    if (shot.IsAccurate) teamMatch.Fg3m++;
                }

                if (request.PlayerAssistId.HasValue)
                {
                    var assist = new Assist { PlayerId = request.PlayerAssistId.Value, Shot = shot };

                    _context.Assist.Add(assist);
                }

                if (request.PlayerReboundId.HasValue || request.TeamReboundId.HasValue)
                {
                    if (request.PlayerReboundId.HasValue)
                    {
                        var rebound = new Rebound { PlayerId = request.PlayerReboundId.Value, Shot = shot, ReboundType = request.ReboundType.Value };
                        _context.Rebound.Add(rebound);
                    }
                    else
                    {
                        var rebound = new Rebound { TeamId = request.TeamReboundId.Value, Shot = shot, ReboundType = request.ReboundType.Value };
                        _context.Rebound.Add(rebound);
                    }
                }

                _context.Shot.Add(shot);


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
