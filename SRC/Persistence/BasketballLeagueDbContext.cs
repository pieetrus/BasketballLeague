using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasketballLeague.Persistence
{
    public class BasketballLeagueDbContext : IdentityDbContext<AppUser>, IBasketballLeagueDbContext
    {

        public BasketballLeagueDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Assist> Assist { get; set; }
        public virtual DbSet<Block> Block { get; set; }
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Foul> Foul { get; set; }
        public virtual DbSet<FreeThrow> FreeThrow { get; set; }
        public virtual DbSet<Incident> Incident { get; set; }
        public virtual DbSet<JumpBall> JumpBall { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerMatch> PlayerMatch { get; set; }
        public virtual DbSet<PlayerSeason> PlayerSeason { get; set; }
        public virtual DbSet<Rebound> Rebound { get; set; }
        public virtual DbSet<Referee> Referee { get; set; }
        public virtual DbSet<RefereeMatches> RefereeMatches { get; set; }
        public virtual DbSet<Season> Season { get; set; }
        public virtual DbSet<SeasonDivision> SeasonDivision { get; set; }
        public virtual DbSet<Shot> Shot { get; set; }
        public virtual DbSet<Steal> Steal { get; set; }
        public virtual DbSet<Substitution> Substitution { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamMatch> TeamMatch { get; set; }
        public virtual DbSet<TeamSeason> TeamSeason { get; set; }
        public virtual DbSet<Timeout> Timeout { get; set; }
        public virtual DbSet<Turnover> Turnover { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BasketballLeagueDbContext).Assembly);
        }


    }
}
