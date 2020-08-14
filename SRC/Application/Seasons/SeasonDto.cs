using BasketballLeague.Application.Divisions;
using System;
using System.Collections.Generic;

namespace BasketballLeague.Application.Seasons
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<DivisionDto> Divisions { get; set; }
    }
}
