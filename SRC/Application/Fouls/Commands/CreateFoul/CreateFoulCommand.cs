using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Fouls.Commands.CreateFoul
{
    public class CreateFoulCommand : IRequest
    {
        public int? PlayerWhoFouledId { get; set; }
        public int? PlayerWhoWasFouledId { get; set; }
        public int? CoachId { get; set; }
        public FoulType FoulType { get; set; }


        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public IncidentType IncidentType { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }


        public class Handler : IRequestHandler<CreateFoulCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateFoulCommand request, CancellationToken cancellationToken)
            {
                if (request.IncidentType != IncidentType.FOUL)
                {
                    throw new Exception("Error creating foul incident - bad incident type");
                }

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = request.IncidentType,
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

                _context.Foul.Add(foul);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}