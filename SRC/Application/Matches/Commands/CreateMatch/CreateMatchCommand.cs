using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : IRequest
    {
        public int SeasonDivisionId { get; set; }
        public int TeamHomeId { get; set; }
        public int TeamGuestId { get; set; }
        public int? Attendance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public class Handler : IRequestHandler<CreateMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
            {

                var entity = new Match
                {
                    SeasonDivisionId = request.SeasonDivisionId,
                    TeamHomeId = request.TeamHomeId,
                    TeamGuestId = request.TeamGuestId,
                    Attendance = request.Attendance ?? 0,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                _context.Match.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
