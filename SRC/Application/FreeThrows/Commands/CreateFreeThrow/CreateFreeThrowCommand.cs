using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.FreeThrows.Commands.CreateFreeThrow
{
    public class CreateFreeThrowCommand : IRequest
    {
        public int PlayerShooterId { get; set; }
        public int AccurateShots { get; set; }
        public int Attempts { get; set; }

        public int? PlayerWhoFouledId { get; set; }
        public int? PlayerWhoWasFouledId { get; set; }
        public int? CoachId { get; set; }
        public FoulType FoulType { get; set; }


        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }


        public class Handler : IRequestHandler<CreateFreeThrowCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateFreeThrowCommand request, CancellationToken cancellationToken)
            {

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.FOUL,
                    Quater = request.Quater,
                    Flagged = request.Flagged
                };

                var foul = new Foul
                {
                    PlayerWhoFouledId = request.PlayerWhoFouledId,
                    PlayerWhoWasFouledId = request.PlayerWhoWasFouledId,
                    CoachId = request.CoachId,
                    FoulType = request.FoulType,
                    Incident = incident
                };

                var freeThrow = new Domain.Entities.FreeThrows
                {
                    PlayerShooterId = request.PlayerShooterId,
                    AccurateShots = request.AccurateShots,
                    Attempts = request.Attempts,
                    Foul = foul
                };

                _context.FreeThrow.Add(freeThrow);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}