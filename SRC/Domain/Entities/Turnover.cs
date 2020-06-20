namespace BasketballLeague.Domain.Entities
{
    public class Turnover
    {
        public int TurnoverId { get; set; }
        public int IncidentId { get; set; }
        public int PlayerId { get; set; }
        public int TurnoverType { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Player Player { get; set; }
        public virtual Steal Steal { get; set; }
    }
}
