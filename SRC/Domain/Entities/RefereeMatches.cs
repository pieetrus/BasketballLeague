﻿namespace BasketballLeague.Domain.Entities
{
    public class RefereeMatches
    {
        public int RefereeId { get; set; }
        public int MatchId { get; set; }

        public virtual Match Match { get; set; }
        public virtual Referee Referee { get; set; }
    }
}