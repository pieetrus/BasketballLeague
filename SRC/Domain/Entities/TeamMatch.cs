namespace BasketballLeague.Domain.Entities
{
    public class TeamMatch
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        //public int MatchId { get; set; }
        public int? Pts { get; private set; }
        public int? Fga { get; private set; }
        public int? Fgm { get; private set; }
        public int Fg3a { get; set; }
        public int Fg3m { get; set; }
        public int Fg2a { get; set; }
        public int Fg2m { get; set; }
        public int Fta { get; set; }
        public int Ftm { get; set; }
        public int? Trb { get; private set; }
        public int Orb { get; set; }
        public int Drb { get; set; }
        public int Ast { get; set; }
        public int Stl { get; set; }
        public int Blk { get; set; }
        public int Tov { get; set; }
        public int Fouls { get; set; }
        public int BenchPts { get; set; }
        public int Fastbreakpoints { get; set; }
        public int SecondChancePoints { get; set; }
        public int PointsFromTurnovers { get; set; }
        public int OffFouls { get; set; }

        public int Fouls1Qtr { get; set; }
        public int Fouls2Qtr { get; set; }
        public int Fouls3Qtr { get; set; }
        public int Fouls4Qtr { get; set; }
        public int Timeouts1Half { get; set; }
        public int Timeouts2Half { get; set; }

        //public virtual Match Match { get; set; }
        //public virtual ICollection<Match> MatchesHome { get; set; }
        //public virtual ICollection<Match> MatchesAway { get; set; }
        public virtual Match MatchHome { get; set; }
        public virtual Match MatchAway { get; set; }
        public virtual Team Team { get; set; }
    }
}
