using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }

        public virtual ICollection<Match> MatchesAway { get; set; }
        public virtual ICollection<Match> MatchesHome { get; set; }
        public virtual ICollection<Rebound> Rebounds { get; set; }
        public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
        public virtual ICollection<TeamMatch> TeamMatches { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Timeout> Timeouts { get; set; }
        public virtual ICollection<Foul> BenchFouls { get; set; }

    }
}
