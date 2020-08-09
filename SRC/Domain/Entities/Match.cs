using System;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int SeasonDivisionId { get; set; }
        public int TeamHomeId { get; set; }
        public int TeamGuestId { get; set; }
        public int Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public bool Ended { get; set; }

        public virtual SeasonDivision SeasonDivision { get; set; }
        public virtual Team TeamGuest { get; set; }
        public virtual Team TeamHome { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; }
        public virtual ICollection<TeamMatch> TeamMatches { get; set; }
        public virtual ICollection<RefereeMatches> RefereeMatches { get; set; }
    }
}
