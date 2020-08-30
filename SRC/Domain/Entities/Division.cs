using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Level { get; set; }

        public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
    }
}
