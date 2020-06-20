using System;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }

        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
    }
}
