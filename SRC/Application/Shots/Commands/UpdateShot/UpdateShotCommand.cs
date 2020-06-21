using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Shots.Commands.UpdateShot
{
    public class UpdateShotCommand : IRequest
    {
        public int Id { get; set; }
        public int? MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int? Quater { get; set; }
        public bool Flagged { get; set; }

        public int? PlayerId { get; set; }
        public ShotType? ShotType { get; set; }
        public bool? IsAccurate { get; set; }
        public bool? IsFastAttack { get; set; }
        public int? Value { get; set; }

        public class Handler : IRequestHandler<UpdateShotCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateShotCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Shot.Include(x => x.Incident).FirstOrDefaultAsync(x => x.ShotId == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Shot), request.Id);
                }

                entity.Incident.MatchId = request.MatchId ?? entity.Incident.MatchId;
                entity.Incident.Minutes = request.Minutes ?? entity.Incident.Minutes;
                entity.Incident.Seconds = request.Seconds ?? entity.Incident.Seconds;
                entity.Incident.Quater = request.Quater ?? entity.Incident.Quater;
                entity.Incident.Flagged = (bool?)request.Flagged ?? entity.Incident.Flagged;

                entity.PlayerId = request.PlayerId ?? entity.PlayerId;
                entity.ShotType = request.ShotType ?? entity.ShotType;
                entity.IsAccurate = request.IsAccurate ?? entity.IsAccurate;
                entity.IsFastAttack = request.IsFastAttack ?? entity.IsFastAttack;
                entity.Value = request.Value ?? entity.Value;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}