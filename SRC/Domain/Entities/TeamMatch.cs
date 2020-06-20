namespace BasketballLeague.Domain.Entities
{
    public class TeamMatch
    {
        public int TeamMatchId { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public int? Pts { get; set; }
        public byte? Fga { get; set; }
        public byte? Fgm { get; set; }
        public byte Fg3a { get; set; }
        public byte Fg3m { get; set; }
        public byte Fg2a { get; set; }
        public byte Fg2m { get; set; }
        public byte Fta { get; set; }
        public byte Ftm { get; set; }
        public byte? Trb { get; set; }
        public byte Orb { get; set; }
        public byte Drb { get; set; }
        public byte Ast { get; set; }
        public byte Stl { get; set; }
        public byte Blk { get; set; }
        public byte Tov { get; set; }
        public byte Fouls { get; set; }
        public byte BenchPts { get; set; }
        public byte Fastbreakpoints { get; set; }
        public byte SecondChancePoints { get; set; }
        public byte PointsFromTurnovers { get; set; }
        public byte OffFouls { get; set; }

        public virtual Match Match { get; set; }
        public virtual Team Team { get; set; }
    }
}
