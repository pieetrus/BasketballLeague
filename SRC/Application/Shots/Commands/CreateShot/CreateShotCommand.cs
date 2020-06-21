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
        public IncidentType IncidentType { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public int PlayerId { get; set; }
        public ShotType ShotType { get; set; }
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
                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.SHOT,
                    Quater = request.Quater,
                    Flagged = request.Flagged
                };

                var shot = new Shot
                {
                    PlayerId = request.PlayerId,
                    ShotType = request.ShotType,
                    IsFastAttack = request.IsFastAttack,
                    IsAccurate = request.IsAccurate,
                    Value = request.Value,
                    Incident = incident,
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
