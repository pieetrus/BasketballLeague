using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : IRequest
    {
        public int DivisionId { get; set; }
        public int TeamHomeId { get; set; }
        public int TeamGuestId { get; set; }
        public int? Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public bool? Ended { get; set; }

        public class Handler : IRequestHandler<CreateMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
            {

                var seasonDivision = await
                    _context.SeasonDivision.
                        Include(x => x.Season)
                        .FirstOrDefaultAsync(x =>
                        x.DivisionId == request.DivisionId
                        && x.Season.StartDate <= request.StartDate &&
                        x.Season.EndDate >= request.StartDate, cancellationToken);

                if (seasonDivision == null)
                    throw new Exception("Matches cannot be scheduled for this season yet. Firstly create season.");

                var entity = new Match
                {
                    SeasonDivisionId = seasonDivision.Id,
                    TeamHomeId = request.TeamHomeId,
                    TeamGuestId = request.TeamGuestId,
                    Attendance = request.Attendance ?? 0,
                    StartDate = request.StartDate,
                    Ended = request.Ended ?? false
                };

                _context.Match.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
