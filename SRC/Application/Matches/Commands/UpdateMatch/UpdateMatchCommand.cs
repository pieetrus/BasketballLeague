using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Commands.UpdateMatch
{
    public class UpdateMatchCommand : IRequest
    {
        public int Id { get; set; }
        public int? SeasonDivisionId { get; set; }
        public int? TeamHomeId { get; set; }
        public int? TeamGuestId { get; set; }
        public int? Attendance { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public class Handler : IRequestHandler<UpdateMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Match
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Match), request.Id);
                }

                entity.SeasonDivisionId = request.SeasonDivisionId ?? entity.SeasonDivisionId;
                entity.TeamHomeId = request.TeamHomeId ?? entity.TeamHomeId;
                entity.TeamGuestId = request.TeamGuestId ?? entity.TeamGuestId;
                entity.Attendance = request.Attendance ?? entity.Attendance;
                entity.StartDate = request.StartDate ?? entity.StartDate;
                entity.EndDate = request.EndDate ?? entity.EndDate;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}


