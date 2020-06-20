using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Foul
    {
        public int FoulId { get; set; }
        public int IncidentId { get; set; }
        public int? PlayerWhoFouledId { get; set; }
        public int? PlayerWhoWasFouledId { get; set; }
        public int FoulType { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Player PlayerWhoFouled { get; set; }
        public virtual Player PlayerWhoWasFouled { get; set; }
        public virtual ICollection<FreeThrow> FreeThrows { get; set; }
    }
}
