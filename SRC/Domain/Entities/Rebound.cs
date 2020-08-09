using BasketballLeague.Domain.Common;

namespace BasketballLeague.Domain.Entities
{
    public class Rebound
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public ReboundType ReboundType { get; set; }
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
