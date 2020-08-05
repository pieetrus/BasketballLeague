using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Timeout = BasketballLeague.Domain.Entities.Timeout;

namespace BasketballLeague.Application.Common.Interfaces
{
    public interface IBasketballLeagueDbContext
    {
         DbSet<Assist> Assist { get; set; }
         DbSet<Block> Block { get; set; }
         DbSet<Coach> Coach { get; set; }
         DbSet<Division> Division { get; set; }
         DbSet<Foul> Foul { get; set; }
         DbSet<FreeThrow> FreeThrow { get; set; }
         DbSet<Incident> Incident { get; set; }
         DbSet<JumpBall> JumpBall { get; set; }
         DbSet<Match> Match { get; set; }
         DbSet<Player> Player { get; set; }
         DbSet<PlayerMatch> PlayerMatch { get; set; }
         DbSet<PlayerSeason> PlayerSeason { get; set; }
         DbSet<Rebound> Rebound { get; set; }
         DbSet<Referee> Referee { get; set; }
         DbSet<RefereeMatches> RefereeMatches { get; set; }
         DbSet<Season> Season { get; set; }
         DbSet<SeasonDivision> SeasonDivision { get; set; }
         DbSet<Shot> Shot { get; set; }
         DbSet<Steal> Steal { get; set; }
         DbSet<Substitution> Substitution { get; set; }
         DbSet<Team> Team { get; set; }
         DbSet<TeamMatch> TeamMatch { get; set; }
         DbSet<TeamSeason> TeamSeason { get; set; }
         DbSet<Timeout> Timeout { get; set; }
         DbSet<Turnover> Turnover { get; set; }
         DbSet<AppUser> AppUser { get; set; }
         DbSet<Photo> Photos { get; set; }

         Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
