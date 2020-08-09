﻿using BasketballLeague.Domain.Common;

namespace BasketballLeague.Domain.Entities
{
    public class JumpBall
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public JumpBallType JumpBallType { get; set; }

        public virtual Incident Incident { get; set; }
    }
}
