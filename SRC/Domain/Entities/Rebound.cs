namespace BasketballLeague.Domain.Entities
{
    public class Rebound
    {
        public int ReboundId { get; set; }
        public int IncidentId { get; set; }
        public int ReboundType { get; set; }
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
