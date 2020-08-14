using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class SeasonDivision
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int DivisionId { get; set; }
        public int? WinnerSeasonDivisionTeamId { get; set; }

        public virtual Division Division { get; set; }
        public virtual Season Season { get; set; }
        public virtual Team WinnerSeasonDivisionTeam { get; set; } // Maybe Team Season????
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
    }
}
