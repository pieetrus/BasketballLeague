﻿namespace BasketballLeague.Domain.Entities
{
    public class Assist
    {
        public int Id { get; set; }
        public int? ShotId { get; set; }
        public int? FreeThrowId { get; set; }
        public int PlayerId { get; set; }

        public virtual FreeThrows FreeThrows { get; set; }
        public virtual Player Player { get; set; }
        public virtual Shot Shot { get; set; }
    }
}
