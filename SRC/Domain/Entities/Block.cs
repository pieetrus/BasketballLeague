namespace BasketballLeague.Domain.Entities
{
    public class Block
    {
        public int BlockId { get; set; }
        public int ShotId { get; set; }
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Shot Shot { get; set; }
    }
}
