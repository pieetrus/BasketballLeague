using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.JumpBalls.Commands.CreateJumpBall
{
    public class CreateJumpBallCommand : IRequest
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public JumpBallType JumpBallType { get; set; }


        public class Handler : IRequestHandler<CreateJumpBallCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateJumpBallCommand request, CancellationToken cancellationToken)
            {
                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.JUMP_BALL,
                    Quater = request.Quater,
                    Flagged = request.Flagged
                };

                var jumpBall = new JumpBall
                {
                    JumpBallType = request.JumpBallType,
                    Incident = incident
                };

                _context.JumpBall.Add(jumpBall);

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