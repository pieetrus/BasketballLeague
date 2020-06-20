namespace BasketballLeague.Domain.Entities
{
    public class Substitution
    {
        public int SubstitutionId { get; set; }
        public int IncidentId { get; set; }
        public int PlayerInId { get; set; }
        public int PlayerOutId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Player PlayerIn { get; set; }
        public virtual Player PlayerOut { get; set; }
    }
}
