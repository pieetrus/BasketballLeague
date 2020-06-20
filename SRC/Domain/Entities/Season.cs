using System;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
    }
}
