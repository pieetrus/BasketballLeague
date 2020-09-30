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
                var match = await _context.Match
                    .Include(x => x.PlayerMatches).ThenInclude(x => x.PlayerSeason)
                    .Include(x => x.TeamGuest)
                    .Include(x => x.TeamHome)
                    .Include(x => x.TeamSeasonGuest)
                    .Include(x => x.TeamSeasonHome)
                    .Include(x => x.Incidents)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (match == null)
                    throw new NotFoundException(nameof(Match), request.Id);

                match.Ended = true;

                //update player season based on player match from current match
                foreach (var playerMatch in match.PlayerMatches)
                {
                    playerMatch.PlayerSeason.Ast += playerMatch.Ast;
                    playerMatch.PlayerSeason.Fg3a += playerMatch.Fg3a;
                    playerMatch.PlayerSeason.Fg3m += playerMatch.Fg3m;
                    playerMatch.PlayerSeason.Fg2m += playerMatch.Fg2m;
                    playerMatch.PlayerSeason.Fg2a += playerMatch.Fg2a;
                    playerMatch.PlayerSeason.Blk += playerMatch.Blk;
                    playerMatch.PlayerSeason.Drb += playerMatch.Drb;
                    playerMatch.PlayerSeason.Orb += playerMatch.Orb;
                    playerMatch.PlayerSeason.Fta += playerMatch.Fta;
                    playerMatch.PlayerSeason.Ftm += playerMatch.Ftm;
                    playerMatch.PlayerSeason.Fouls += playerMatch.Fouls;
                    playerMatch.PlayerSeason.OffFouls += playerMatch.OffFouls;
                    playerMatch.PlayerSeason.Tov += playerMatch.Tov;
                    playerMatch.PlayerSeason.Stl += playerMatch.Stl;
                }

                //update team home season based on team match from current match
                match.TeamSeasonHome.Ast += match.TeamHome.Ast;
                match.TeamSeasonHome.Fg3a += match.TeamHome.Fg3a;
                match.TeamSeasonHome.Fg3m += match.TeamHome.Fg3m;
                match.TeamSeasonHome.Fg2m += match.TeamHome.Fg2m;
                match.TeamSeasonHome.Fg2a += match.TeamHome.Fg2a;
                match.TeamSeasonHome.Blk += match.TeamHome.Blk;
                match.TeamSeasonHome.Drb += match.TeamHome.Drb;
                match.TeamSeasonHome.Orb += match.TeamHome.Orb;
                match.TeamSeasonHome.Fta += match.TeamHome.Fta;
                match.TeamSeasonHome.Ftm += match.TeamHome.Ftm;
                match.TeamSeasonHome.Fouls += match.TeamHome.Fouls;
                match.TeamSeasonHome.OffFouls += match.TeamHome.OffFouls;
                match.TeamSeasonHome.Tov += match.TeamHome.Tov;
                match.TeamSeasonHome.Stl += match.TeamHome.Stl;

                //update team home season based on team match from current match
                match.TeamSeasonGuest.Ast += match.TeamGuest.Ast;
                match.TeamSeasonGuest.Fg3a += match.TeamGuest.Fg3a;
                match.TeamSeasonGuest.Fg3m += match.TeamGuest.Fg3m;
                match.TeamSeasonGuest.Fg2m += match.TeamGuest.Fg2m;
                match.TeamSeasonGuest.Fg2a += match.TeamGuest.Fg2a;
                match.TeamSeasonGuest.Blk += match.TeamGuest.Blk;
                match.TeamSeasonGuest.Drb += match.TeamGuest.Drb;
                match.TeamSeasonGuest.Orb += match.TeamGuest.Orb;
                match.TeamSeasonGuest.Fta += match.TeamGuest.Fta;
                match.TeamSeasonGuest.Ftm += match.TeamGuest.Ftm;
                match.TeamSeasonGuest.Fouls += match.TeamGuest.Fouls;
                match.TeamSeasonGuest.OffFouls += match.TeamGuest.OffFouls;
                match.TeamSeasonGuest.Tov += match.TeamGuest.Tov;
                match.TeamSeasonGuest.Stl += match.TeamGuest.Stl;

                if (match.TeamHome.Pts > match.TeamGuest.Pts)
                {
                    match.TeamSeasonHome.RankingPoints += 2;
                }
                else
                {
                    match.TeamSeasonGuest.RankingPoints += 2;
                }

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
