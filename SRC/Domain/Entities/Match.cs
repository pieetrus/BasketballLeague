using System;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int SeasonDivisionId { get; set; }
        public int? TeamHomeId { get; set; }
        public int? TeamGuestId { get; set; }
        public int TeamSeasonHomeId { get; set; }
        public int TeamSeasonGuestId { get; set; }
        public int Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }
        public string TeamHomeJerseyColor { get; set; }
        public string TeamGuestJerseyColor { get; set; }


        public SeasonDivision SeasonDivision { get; set; }
        public virtual TeamMatch TeamGuest { get; set; }
        public virtual TeamMatch TeamHome { get; set; }
        public virtual TeamSeason TeamSeasonGuest { get; set; }
        public virtual TeamSeason TeamSeasonHome { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; }
    }
}
