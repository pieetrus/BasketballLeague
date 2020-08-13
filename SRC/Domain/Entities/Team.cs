using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public Photo Logo { get; set; }
        public virtual ICollection<Rebound> Rebounds { get; set; }
        public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
        public virtual ICollection<TeamMatch> TeamMatches { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Timeout> Timeouts { get; set; }
        public virtual ICollection<Foul> BenchFouls { get; set; }

    }
}
