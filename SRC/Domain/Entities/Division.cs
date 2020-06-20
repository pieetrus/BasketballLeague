using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
    }
}
