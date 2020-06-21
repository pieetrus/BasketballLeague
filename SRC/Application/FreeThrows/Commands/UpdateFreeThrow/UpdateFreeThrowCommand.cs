using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.FreeThrows.Commands.UpdateFreeThrow
{
    public class UpdateFreeThrowCommand : IRequest
    {
        public int? PlayerShooterId { get; set; }
        public int? AccurateShots { get; set; }
        public int? Attempts { get; set; }

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


        public class Handler : IRequestHandler<UpdateFreeThrowCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateFreeThrowCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.FreeThrow
                    .Include(x => x.Foul)
                    .ThenInclude(x => x.Incident)
                    .SingleOrDefaultAsync(x => x.FoulId == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(FreeThrow), request.Id);
                }

                entity.Foul.Incident.MatchId = request.MatchId ?? entity.Foul.Incident.MatchId;
                entity.Foul.Incident.Minutes = request.Minutes ?? entity.Foul.Incident.Minutes;
                entity.Foul.Incident.Seconds = request.Seconds ?? entity.Foul.Incident.Seconds;
                entity.Foul.Incident.Quater = request.Quater ?? entity.Foul.Incident.Quater;
                entity.Foul.Incident.Flagged = request.Flagged ?? entity.Foul.Incident.Flagged;

                entity.Foul.PlayerWhoFouledId = request.PlayerWhoFouledId ?? entity.Foul.PlayerWhoFouledId;
                entity.Foul.PlayerWhoWasFouledId = request.PlayerWhoWasFouledId ?? entity.Foul.PlayerWhoWasFouledId;
                entity.Foul.CoachId = request.CoachId ?? entity.Foul.CoachId;
                entity.Foul.FoulType = request.FoulType ?? entity.Foul.FoulType;

                entity.PlayerShooterId = request.PlayerShooterId ?? entity.PlayerShooterId;
                entity.AccurateShots = request.AccurateShots ?? entity.AccurateShots;
                entity.Attempts = request.Attempts ?? entity.Attempts;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}