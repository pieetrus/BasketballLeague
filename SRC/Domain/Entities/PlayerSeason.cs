namespace BasketballLeague.Domain.Entities
{
    public class PlayerSeason
    {
        public int PlayerSeason1 { get; set; }
        public int PlayerId { get; set; }
        public int SeasonDivisionId { get; set; }
        public int? Pts { get; set; }
        public short? Fga { get; set; }
        public short? Fgm { get; set; }
        public short Fg3a { get; set; }
        public short Fg3m { get; set; }
        public short Fg2a { get; set; }
        public short Fg2m { get; set; }
        public short Fta { get; set; }
        public short Ftm { get; set; }
        public short? Trb { get; set; }
        public short Orb { get; set; }
        public short Drb { get; set; }
        public short Ast { get; set; }
        public short Stl { get; set; }
        public short Blk { get; set; }
        public short Tov { get; set; }
        public short Fouls { get; set; }
        public short OffFouls { get; set; }
        public int? TeamId { get; set; }
        public string JerseyNr { get; set; }

        public virtual Player Player { get; set; }
        public virtual SeasonDivision SeasonDivision { get; set; }
    }
}
