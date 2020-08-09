using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Fouls.Commands.UpdateFoul
{
    public class UpdateFoulCommand : IRequest
    {
        public int Id { get; set; }
        public int? PlayerWhoFouledId { get; set; }
        public int? PlayerWhoWasFouledId { get; set; }
        public int? CoachId { get; set; }
        public FoulType? FoulType { get; set; }


        public int? MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int? Quater { get; set; }
        public bool? Flagged { get; set; }

        public class Handler : IRequestHandler<UpdateFoulCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateFoulCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Foul
                    .Include(x => x.Incident)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Foul), request.Id);
                }

                entity.Incident.MatchId = request.MatchId ?? entity.Incident.MatchId;
                entity.Incident.Minutes = request.Minutes ?? entity.Incident.Minutes;
                entity.Incident.Seconds = request.Seconds ?? entity.Incident.Seconds;
                entity.Incident.Quater = request.Quater ?? entity.Incident.Quater;
                entity.Incident.Flagged = request.Flagged ?? entity.Incident.Flagged;

                entity.PlayerWhoFouledId = request.PlayerWhoFouledId ?? entity.PlayerWhoFouledId;
                entity.PlayerWhoWasFouledId = request.PlayerWhoWasFouledId ?? entity.PlayerWhoWasFouledId;
                entity.CoachId = request.CoachId ?? entity.CoachId;
                entity.FoulType = request.FoulType ?? entity.FoulType;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}