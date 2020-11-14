using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Incidents.Commands.DeleteIncident
{
    public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand>
    {
        private readonly IBasketballLeagueDbContext _context;

        public DeleteIncidentCommandHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Incident
                .Include(x => x.Shot)
                .Include(x => x.Foul).ThenInclude(x => x.FreeThrows).ThenInclude(x => x.Assist)
                .Include(x => x.Foul).ThenInclude(x => x.FreeThrows).ThenInclude(x => x.Rebound)
                .Include(x => x.Turnover).ThenInclude(x => x.Steal)
                .Include(x => x.Timeout)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Incident), request.Id);
            }

            if (entity.IncidentType == IncidentType.SHOT)
            {
                var team = await _context.Match
                    .Include(x => x.TeamGuest)
                    .Include(x => x.TeamHome)
                    .FirstOrDefaultAsync(x => x.Id == entity.MatchId, cancellationToken);

                var teamMatch = entity.IsGuest ? team.TeamGuest : team.TeamHome;

                if (entity.Shot.Value == 2)
                {
                    teamMatch.Fg2a--;
                    if (entity.Shot.IsAccurate) teamMatch.Fg2m--;
                }
                else if (entity.Shot.Value == 3)
                {
                    teamMatch.Fg3a--;
                    if (entity.Shot.IsAccurate) teamMatch.Fg3m--; ;

                }
            }

            if (entity.IncidentType == IncidentType.FOUL)
            {
                var match = await _context.Match
                    .Include(x => x.TeamGuest)
                    .Include(x => x.TeamHome)
                    .FirstOrDefaultAsync(x => x.Id == entity.MatchId, cancellationToken);

                var teamMatch = entity.IsGuest ? match.TeamGuest : match.TeamHome;

                TeamMatch teamMatchWhoFouled;
                TeamMatch teamMatchWhoWasFouled;
                if (entity.IsGuest)
                {
                    teamMatchWhoFouled = match.TeamGuest;
                    teamMatchWhoWasFouled = match.TeamHome;
                }
                else
                {
                    teamMatchWhoFouled = match.TeamHome;
                    teamMatchWhoWasFouled = match.TeamGuest;
                }

                switch (entity.Quater)
                {
                    case 1:
                        teamMatch.Fouls1Qtr--;
                        break;
                    case 2:
                        teamMatch.Fouls2Qtr--;
                        break;
                    case 3:
                        teamMatch.Fouls3Qtr--;
                        break;
                    case 4:
                        teamMatch.Fouls4Qtr--;
                        break;
                }

                var playerMatchWhoFouled = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                    x => x.PlayerSeason.PlayerId == entity.Foul.PlayerWhoFouledId && x.MatchId == entity.MatchId, cancellationToken);

                var playerMatchWhoWasFouled = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                    x => x.PlayerSeason.PlayerId == entity.Foul.PlayerWhoWasFouledId && x.MatchId == entity.MatchId, cancellationToken);

                if (entity.Foul.FoulType == FoulType.OFFENSIVE)
                {
                    playerMatchWhoFouled.OffFouls--;
                    teamMatchWhoFouled.OffFouls--;
                }
                playerMatchWhoFouled.Fouls--;
                teamMatchWhoFouled.Fouls--;

                if (entity.Foul.FreeThrows.Attempts > 0)
                {
                    teamMatchWhoWasFouled.Fta -= entity.Foul.FreeThrows.Attempts;
                    teamMatchWhoWasFouled.Ftm -= entity.Foul.FreeThrows.AccurateShots;

                    playerMatchWhoWasFouled.Fta -= entity.Foul.FreeThrows.Attempts;
                    playerMatchWhoWasFouled.Ftm -= entity.Foul.FreeThrows.AccurateShots;


                    if (entity.Foul.FreeThrows.AccurateShots > 0 && entity.Foul.FreeThrows.Assist != null)
                    {
                        var playerMatchAssist = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                            x => x.PlayerSeason.PlayerId == entity.Foul.FreeThrows.Assist.PlayerId && x.MatchId == entity.MatchId, cancellationToken);

                        playerMatchAssist.Ast--;
                        teamMatchWhoWasFouled.Ast--;
                    }

                    if (entity.Foul.FreeThrows.Rebound != null)
                    {
                        var playerMatchRebound = await _context.PlayerMatch.Include(x => x.PlayerSeason).FirstOrDefaultAsync(
                            x => x.PlayerSeason.PlayerId == entity.Foul.FreeThrows.Rebound.PlayerId && x.MatchId == entity.MatchId, cancellationToken);

                        if (entity.Foul.FreeThrows.Rebound.ReboundType == ReboundType.PLAYER_DEF)
                        {
                            playerMatchRebound.Drb--;
                            teamMatchWhoFouled.Drb--;
                        }
                        else if (entity.Foul.FreeThrows.Rebound.ReboundType == ReboundType.PLAYER_OFF)
                        {
                            playerMatchRebound.Orb--;
                            teamMatchWhoWasFouled.Orb--;
                        }
                        else if (entity.Foul.FreeThrows.Rebound.ReboundType == ReboundType.TEAM_DEF)
                        {
                            teamMatchWhoFouled.Drb--;
                        }
                        else if (entity.Foul.FreeThrows.Rebound.ReboundType == ReboundType.TEAM_OFF)
                        {
                            teamMatchWhoWasFouled.Orb--;
                        }
                    }
                }

                if (entity.Foul.FreeThrows.Assist != null)
                {
                    _context.Assist.Remove(entity.Foul.FreeThrows.Assist);
                }

                if (entity.Foul.FreeThrows.Rebound != null)
                {
                    _context.Rebound.Remove(entity.Foul.FreeThrows.Rebound);
                }
            }

            if (entity.IncidentType == IncidentType.TURNOVER)
            {
                TeamMatch teamMatchWhoTurnover;
                TeamMatch teamMatchWhoSteal;

                var playerMatchTurnover = await _context.PlayerMatch
                    .Include(x => x.PlayerSeason)
                    .FirstOrDefaultAsync(x => x.PlayerSeason.PlayerId == entity.Turnover.PlayerId && x.MatchId == entity.MatchId, cancellationToken);


                var match = await _context.Match
                    .Include(x => x.TeamGuest)
                    .Include(x => x.TeamHome)
                    .FirstOrDefaultAsync(x => x.Id == entity.MatchId, cancellationToken);

                if (entity.IsGuest)
                {
                    teamMatchWhoTurnover = match.TeamGuest;
                    teamMatchWhoSteal = match.TeamHome;
                }
                else
                {
                    teamMatchWhoTurnover = match.TeamHome;
                    teamMatchWhoSteal = match.TeamGuest;
                }

                playerMatchTurnover.Tov--;
                teamMatchWhoTurnover.Tov--;

                if (entity.Turnover.Steal != null)
                {
                    var playerMatchSteal = await _context.PlayerMatch
                        .Include(x => x.PlayerSeason)
                        .FirstOrDefaultAsync(x => x.PlayerSeason.PlayerId == entity.Turnover.Steal.PlayerId && x.MatchId == entity.MatchId, cancellationToken);

                    playerMatchSteal.Stl--;
                    teamMatchWhoSteal.Stl--;
                }

                if (entity.Turnover.Steal != null)
                {
                    _context.Steal.Remove(entity.Turnover.Steal);
                }
            }

            if (entity.IncidentType == IncidentType.TIMEOUT)
            {
                TeamMatch teamMatch;

                if (entity.IsGuest)
                {
                    teamMatch = await _context.TeamMatch.FirstOrDefaultAsync(x =>
                        x.TeamId == entity.Timeout.TeamId && x.MatchAway.Id == entity.MatchId);
                }
                else
                {
                    teamMatch = await _context.TeamMatch.FirstOrDefaultAsync(x =>
                        x.TeamId == entity.Timeout.TeamId && x.MatchHome.Id == entity.MatchId);
                }

                if (entity.Quater <= 2)
                {
                    teamMatch.Timeouts1Half--;
                }
                else
                {
                    teamMatch.Timeouts2Half--;
                }
            }

            _context.Incident.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
