using BasketballLeague.Application.PlayerMatches;
using System;
using System.Collections.Generic;

namespace BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed
{
    public class MatchDetailedDto
    {
        public int Id { get; set; }
        public string Division { get; set; }
        public string TeamHome { get; set; }
        public string TeamGuest { get; set; }
        public IEnumerable<PlayerBeforeMatchDto> TeamHomePlayers { get; set; }
        public IEnumerable<PlayerBeforeMatchDto> TeamGuestPlayers { get; set; }
        public int Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public bool Ended { get; set; }
    }
}
