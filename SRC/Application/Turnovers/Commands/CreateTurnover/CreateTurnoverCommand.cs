using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Commands.CreateTurnover
{
    public class CreateTurnoverCommand : IRequest<int>
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }
        public bool IsGuest { get; set; }

        public int PlayerId { get; set; }
        public TurnoverType TurnoverType { get; set; }



        public class Handler : IRequestHandler<CreateTurnoverCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateTurnoverCommand request, CancellationToken cancellationToken)
            {
                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = IncidentType.TURNOVER,
                    Quater = request.Quater,
                    Flagged = request.Flagged,
                    IsGuest = request.IsGuest
                };

                var turnover = new Turnover
                {
                    PlayerId = request.PlayerId,
                    TurnoverType = request.TurnoverType,
                    Incident = incident
                };

                _context.Turnover.Add(turnover);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    return incident.Id;
                }

                throw new Exception("Error saving changes");
            }
        }
    }
}
