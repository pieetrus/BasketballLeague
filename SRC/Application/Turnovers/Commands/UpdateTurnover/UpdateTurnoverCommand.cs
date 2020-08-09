using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Commands.UpdateTimeout
{
    public class UpdateTurnoverCommand : IRequest
    {
        public int Id { get; set; }
        public int? MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int? Quater { get; set; }
        public bool? Flagged { get; set; }

        public int? PlayerId { get; set; }
        public TurnoverType? TurnoverType { get; set; }

        public class Handler : IRequestHandler<UpdateTurnoverCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTurnoverCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Turnover.Include(x => x.Incident).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Turnover), request.Id);
                }

                entity.Incident.MatchId = request.MatchId ?? entity.Incident.MatchId;
                entity.Incident.Minutes = request.Minutes ?? entity.Incident.Minutes;
                entity.Incident.Seconds = request.Seconds ?? entity.Incident.Seconds;
                entity.Incident.Quater = request.Quater ?? entity.Incident.Quater;
                entity.Incident.Flagged = request.Flagged ?? entity.Incident.Flagged;

                entity.PlayerId = request.PlayerId ?? entity.PlayerId;
                entity.TurnoverType = request.TurnoverType ?? entity.TurnoverType;

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