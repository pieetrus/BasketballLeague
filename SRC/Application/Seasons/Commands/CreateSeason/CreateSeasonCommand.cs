using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommand : IRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public class Handler : IRequestHandler<CreateSeasonCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
            {
                var entity = new Season
                {
                    Name = request.Name,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                _context.Season.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
