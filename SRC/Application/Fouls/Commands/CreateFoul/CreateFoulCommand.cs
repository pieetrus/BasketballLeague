using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Fouls.Commands.CreateFoul
{
    public class CreateFoulCommand : IRequest<int>
    {
        public int? PlayerWhoFouledId { get; set; }
        public int? PlayerWhoWasFouledId { get; set; }
        public int? CoachId { get; set; }
        public FoulType FoulType { get; set; }


        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }
        public bool IsGuest { get; set; }

        public int? PlayerAssistId { get; set; }
        public int? PlayerReboundId { get; set; }
        public int? TeamReboundId { get; set; }
        public ReboundType? ReboundType { get; set; }


        public int? AccurateShots { get; set; }
        public int? Attempts { get; set; }


        public class Handler : IRequestHandler<CreateFoulCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateFoulCommand request, CancellationToken cancellationToken)
            {

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.FOUL,
                    Quater = request.Quater,
                    Flagged = request.Flagged,
                    IsGuest = request.IsGuest
                };

                var foul = new Foul
                {
                    PlayerWhoFouledId = request.PlayerWhoFouledId,
                    PlayerWhoWasFouledId = request.PlayerWhoWasFouledId,
                    CoachId = request.CoachId,
                    FoulType = request.FoulType,
                    Incident = incident
                };

                if (request.AccurateShots.HasValue && request.Attempts.HasValue)
                {
                    TeamMatch match;
                    if (request.IsGuest) // check if foul was from guest player
                    {
                        match = _context.Match
                            .Include(x => x.TeamHome)
                            .FirstOrDefault(x => x.Id == request.MatchId).TeamHome;
                    }
                    else
                    {
                        match = _context.Match
                            .Include(x => x.TeamGuest)
                            .FirstOrDefault(x => x.Id == request.MatchId).TeamGuest;
                    }

                    match.Fta += request.Attempts.Value;
                    match.Ftm += request.AccurateShots.Value;

                    var freeThrow = new Domain.Entities.FreeThrows
                    {
                        AccurateShots = request.AccurateShots.Value,
                        Attempts = request.Attempts.Value,
                        Foul = foul,
                        PlayerShooterId = request.PlayerWhoWasFouledId.Value
                    };

                    _context.FreeThrow.Add(freeThrow);

                    if (request.PlayerAssistId.HasValue)
                    {
                        var assist = new Assist { FreeThrows = freeThrow, PlayerId = request.PlayerAssistId.Value };

                        _context.Assist.Add(assist);
                    }

                    if (request.PlayerReboundId.HasValue || request.TeamReboundId.HasValue)
                    {
                        var rebound = new Rebound
                        {
                            FreeThrows = freeThrow,
                            ReboundType = request.ReboundType.Value
                        };
                        if (request.PlayerReboundId.HasValue) rebound.PlayerId = request.PlayerReboundId.Value;
                        else rebound.TeamId = request.TeamReboundId.Value;

                        _context.Rebound.Add(rebound);
                    }
                }

                _context.Foul.Add(foul);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return incident.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}