using BasketballLeague.Domain.Common;

namespace BasketballLeague.Domain.Entities
{
    public class Rebound
    {
        public int Id { get; set; }
        public int? ShotId { get; set; }
        public int? FreeThrowId { get; set; }
        public ReboundType ReboundType { get; set; }
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
        public virtual Shot Shot { get; set; }
        public virtual FreeThrow FreeThrow { get; set; }
    }
}
