using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Shots.Commands.CreateShot
{
    public class CreateShotCommand : IRequest
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int IncidentType { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public int PlayerId { get; set; }
        public int ShotType { get; set; }
        public bool IsAccurate { get; set; }
        public bool IsFastAttack { get; set; }
        public int Value { get; set; }


        public class Handler : IRequestHandler<CreateShotCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateShotCommand request, CancellationToken cancellationToken)
            {
                if (request.IncidentType != (int)BasketballLeague.Domain.Common.IncidentType.SHOT)
                {
                    throw new Exception("Error creating shot incident - bad incident type");
                }

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = (IncidentType)request.IncidentType,
                    Quater = request.Quater,
                    Flagged = request.Flagged
                };

                var shot = new Shot
                {
                    PlayerId = request.PlayerId,
                    ShotType = (ShotType)request.ShotType,
                    IsAccurate = request.IsAccurate,
                    IsFastAttack = request.IsFastAttack,
                    Value = request.Value,
                    Incident = incident,
                    IncidentId = incident.IncidentId,
                };

                _context.Shot.Add(shot);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    return Unit.Value;
                }

                throw new Exception("Error saving changes");
            }
        }
    }
}
