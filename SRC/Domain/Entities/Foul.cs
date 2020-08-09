using BasketballLeague.Domain.Common;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class Foul
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int? PlayerWhoFouledId { get; set; }
        public int? PlayerWhoWasFouledId { get; set; }
        public int? CoachId { get; set; }
        public int? TeamId { get; set; }
        public FoulType FoulType { get; set; }

        public virtual Coach Coach { get; set; }
        public virtual Team Team { get; set; }
        public virtual Incident Incident { get; set; }
        public virtual Player PlayerWhoFouled { get; set; }
        public virtual Player PlayerWhoWasFouled { get; set; }
        public virtual ICollection<FreeThrow> FreeThrows { get; set; }
    }
}
