namespace BasketballLeague.Domain.Entities
{
    public class Steal
    {
        public int Id { get; set; }
        public int TurnoverId { get; set; }
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Turnover Turnover { get; set; }
    }
}
