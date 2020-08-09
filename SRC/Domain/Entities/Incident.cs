using BasketballLeague.Domain.Common;

namespace BasketballLeague.Domain.Entities
{
    public class Incident
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public IncidentType IncidentType { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public virtual Match Match { get; set; }
        public virtual Foul Foul { get; set; }
        public virtual Rebound Rebound { get; set; }
        public virtual Shot Shot { get; set; }
        public virtual Substitution Substitution { get; set; }
        public virtual Timeout Timeout { get; set; }
        public virtual Turnover Turnover { get; set; }
        public virtual JumpBall JumpBall { get; set; }
    }
}
