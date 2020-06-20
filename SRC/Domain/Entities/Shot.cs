using BasketballLeague.Domain.Common;

namespace BasketballLeague.Domain.Entities
{
    public class Shot
    {
        public int ShotId { get; set; }
        public int IncidentId { get; set; }
        public int PlayerId { get; set; }
        public ShotType ShotType { get; set; }
        public bool IsAccurate { get; set; }
        public bool IsFastAttack { get; set; }
        public int Value { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Player Player { get; set; }
        public virtual Assist Assist { get; set; }
        public virtual Block Block { get; set; }
    }
}
