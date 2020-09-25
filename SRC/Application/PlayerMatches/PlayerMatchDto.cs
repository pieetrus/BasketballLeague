namespace BasketballLeague.Application.PlayerMatches
{
    public class PlayerMatchDto
    {
        public int Id { get; set; }
        public int PlayerSeasonId { get; set; }
        public int MatchId { get; set; }
        public bool IsGuest { get; set; }
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
        public int OffFouls { get; set; }
        public PlayerBeforeMatchDto Player { get; set; }
    }
}
