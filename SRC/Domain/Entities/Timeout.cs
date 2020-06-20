namespace BasketballLeague.Domain.Entities
{
    public class Timeout
    {
        public int TimeoutId { get; set; }
        public int IncidentId { get; set; }
        public int TeamId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Team Team { get; set; }
    }
}
