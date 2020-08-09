namespace BasketballLeague.Domain.Entities
{
    public class FreeThrow
    {
        public int Id { get; set; }
        public int FoulId { get; set; }
        public int PlayerShooterId { get; set; }
        public int AccurateShots { get; set; }
        public int Attempts { get; set; }

        public virtual Foul Foul { get; set; }
        public virtual Player PlayerShooter { get; set; }
        public virtual Assist Assist { get; set; }
    }
}
