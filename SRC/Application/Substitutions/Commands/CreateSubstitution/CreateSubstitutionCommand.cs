using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Substitutions.Commands.CreateSubstitution
{
    public class CreateSubstitutionCommand : IRequest
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public int PlayerInId { get; set; }
        public int PlayerOutId { get; set; }


        public class Handler : IRequestHandler<CreateSubstitutionCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateSubstitutionCommand request, CancellationToken cancellationToken)
            {
                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.SUBSTITUTION,
                    Quater = request.Quater,
                    Flagged = request.Flagged
                };

                var substitution = new Substitution
                {
                    PlayerInId = request.PlayerInId,
                    PlayerOutId = request.PlayerOutId,
                    Incident = incident
                };

                _context.Substitution.Add(substitution);

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