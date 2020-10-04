using BasketballLeague.Domain.Entities;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class TeamSeasonListDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int SeasonDivisionId { get; set; }
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
        public int? CoachId { get; set; }
        public int? CapitainId { get; set; }
        public int RankingPoints { get; set; }
        public string Season { get; set; }
        public string Division { get; set; }
        public int Wins { get; set; }
        public int Lost { get; set; }
        public virtual Team Team { get; set; }
    }
}
