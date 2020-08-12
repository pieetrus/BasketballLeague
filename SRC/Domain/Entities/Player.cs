using BasketballLeague.Domain.Common;
using System;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }
        public int? Height { get; set; }
        public Postition? Position { get; set; }
        public AppUser AppUser { get; set; }

        public virtual ICollection<Assist> Assists { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
        public virtual ICollection<Foul> Fouls { get; set; }
        public virtual ICollection<Foul> FoulsOn { get; set; }
        public virtual ICollection<FreeThrow> FreeThrows { get; set; }
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
        public virtual ICollection<Rebound> Rebounds { get; set; }
        public virtual ICollection<Shot> Shots { get; set; }
        public virtual ICollection<Steal> Steals { get; set; }
        public virtual ICollection<Substitution> SubstitutionIn { get; set; }
        public virtual ICollection<Substitution> SubstitutionOut { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Turnover> Turnovers { get; set; }
    }
}
