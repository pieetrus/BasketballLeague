using System;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Referee
    {
        public int RefereeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JerseyNr { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }

        public virtual ICollection<RefereeMatches> RefereeMatches { get; set; }
    }
}
