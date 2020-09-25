using System;

namespace BasketballLeague.Application.Matches.Queries.GetDetailedMatchesList
{
    public class MatchListDto
    {
        public int Id { get; set; }
        public string Division { get; set; }
        public string TeamHome { get; set; }
        public string TeamGuest { get; set; }
        public int TeamHomePts { get; set; }
        public int TeamGuestPts { get; set; }
        public int Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public bool Ended { get; set; }
        public bool Started { get; set; }
        public string TeamHomeJerseyColor { get; set; }
        public string TeamGuestJerseyColor { get; set; }
    }
}
