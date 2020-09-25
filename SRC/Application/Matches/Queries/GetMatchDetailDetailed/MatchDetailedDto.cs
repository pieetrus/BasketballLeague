using BasketballLeague.Application.PlayerMatches;
using BasketballLeague.Application.Teams;
using System;
using System.Collections.Generic;

namespace BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed
{
    public class MatchDetailedDto
    {
        public int Id { get; set; }
        public string Division { get; set; }
        public TeamDto TeamHome { get; set; }
        public TeamDto TeamGuest { get; set; }
        public IEnumerable<PlayerBeforeMatchDto> TeamHomePlayers { get; set; }
        public IEnumerable<PlayerBeforeMatchDto> TeamGuestPlayers { get; set; }
        public List<int> PlayersInGameIds { get; set; }
        public int TeamHomePts { get; set; }
        public int TeamGuestPts { get; set; }
        public string LastIncidentMinutes { get; set; }
        public string LastIncidentSeconds { get; set; }
        public int LastIncidentQuater { get; set; }
        public int Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }
        public string TeamHomeJerseyColor { get; set; }
        public string TeamGuestJerseyColor { get; set; }

    }
}
