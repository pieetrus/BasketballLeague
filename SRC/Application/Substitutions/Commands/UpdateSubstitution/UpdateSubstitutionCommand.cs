using BasketballLeague.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Substitutions.Commands.UpdateSubstitution
{
    public class UpdateSubstitutionCommand : IRequest
    {
        public int Id { get; set; }
        public int? MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public int? Quater { get; set; }
        public bool? Flagged { get; set; }

        public int? PlayerInId { get; set; }
        public int? PlayerOutId { get; set; }


        public class Handler : IRequestHandler<UpdateSubstitutionCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateSubstitutionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Substitution.Include(x => x.Incident).FirstOrDefaultAsync(x => x.Id == request.Id);

                entity.Incident.MatchId = request.MatchId ?? entity.Incident.MatchId;
                entity.Incident.Minutes = request.Minutes ?? entity.Incident.Minutes;
                entity.Incident.Seconds = request.Seconds ?? entity.Incident.Seconds;
                entity.Incident.Quater = request.Quater ?? entity.Incident.Quater;
                entity.Incident.Flagged = request.Flagged ?? entity.Incident.Flagged;

                entity.PlayerInId = request.PlayerInId ?? entity.PlayerInId;
                entity.PlayerOutId = request.PlayerOutId ?? entity.PlayerOutId;

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
