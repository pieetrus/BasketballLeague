using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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

                switch (request.Quater)
                {
                    case 1:
                        teamMatch.Fouls1Qtr++;
                        break;
                    case 2:
                        teamMatch.Fouls2Qtr++;
                        break;
                    case 3:
                        teamMatch.Fouls3Qtr++;
                        break;
                    case 4:
                        teamMatch.Fouls4Qtr++;
                        break;
                }

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

                var playerMatchWhoFouled = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                    x => x.PlayerSeason.PlayerId == request.PlayerWhoFouledId && x.MatchId == request.MatchId, cancellationToken);

                var playerMatchWhoWasFouled = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                    x => x.PlayerSeason.PlayerId == request.PlayerWhoWasFouledId && x.MatchId == request.MatchId, cancellationToken);

                if (playerMatchWhoFouled == null)
                {
                    var playerSeason =
                        await _context.PlayerSeason.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerWhoFouledId,
                            cancellationToken);

                    playerMatchWhoFouled = new PlayerMatch
                    { PlayerSeasonId = playerSeason.Id, MatchId = request.MatchId };
                    _context.PlayerMatch.Add(playerMatchWhoFouled);
                }

                if (playerMatchWhoWasFouled == null)
                {
                    var playerSeason =
                        await _context.PlayerSeason.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerWhoWasFouledId,
                            cancellationToken);

                    playerMatchWhoWasFouled = new PlayerMatch
                    { PlayerSeasonId = playerSeason.Id, MatchId = request.MatchId };
                    _context.PlayerMatch.Add(playerMatchWhoWasFouled);
                }


                if (request.FoulType == FoulType.OFFENSIVE)
                    playerMatchWhoFouled.OffFouls++;
                playerMatchWhoFouled.Fouls++;


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

                    teamMatch.Fta += request.Attempts.Value;
                    teamMatch.Ftm += request.AccurateShots.Value;

                    playerMatchWhoWasFouled.Fta = request.Attempts.Value;
                    playerMatchWhoWasFouled.Ftm = request.AccurateShots.Value;

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

                        var playerMatchAssist = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                            x => x.PlayerSeason.PlayerId == request.PlayerAssistId && x.MatchId == request.MatchId, cancellationToken);

                        if (playerMatchAssist == null)
                        {
                            var playerSeason =
                                await _context.PlayerSeason.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerAssistId,
                                    cancellationToken);

                            playerMatchAssist = new PlayerMatch
                            { PlayerSeasonId = playerSeason.Id, MatchId = request.MatchId };
                            _context.PlayerMatch.Add(playerMatchAssist);
                        }

                        playerMatchAssist.Ast++;

                        _context.Assist.Add(assist);
                    }

                    if (request.PlayerReboundId.HasValue || request.TeamReboundId.HasValue)
                    {
                        var rebound = new Rebound
                        {
                            FreeThrows = freeThrow,
                            ReboundType = request.ReboundType.Value
                        };


                        if (request.PlayerReboundId.HasValue)
                        {
                            rebound.PlayerId = request.PlayerReboundId.Value;

                            var playerMatchRebound = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                                x => x.PlayerSeason.PlayerId == request.PlayerReboundId && x.MatchId == request.MatchId, cancellationToken);

                            if (playerMatchRebound == null)
                            {
                                var playerSeason =
                                    await _context.PlayerSeason.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerReboundId,
                                        cancellationToken);

                                playerMatchRebound = new PlayerMatch
                                { PlayerSeasonId = playerSeason.Id, MatchId = request.MatchId };
                                _context.PlayerMatch.Add(playerMatchRebound);
                            }

                            if (request.ReboundType == Domain.Common.ReboundType.PLAYER_DEF) playerMatchRebound.Drb++;
                            else playerMatchRebound.Orb++;
                        }
                        else
                        {
                            rebound.TeamId = request.TeamReboundId.Value;

                            if (request.ReboundType == Domain.Common.ReboundType.TEAM_DEF) teamMatch.Drb++;
                            else teamMatch.Orb++;
                        }

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