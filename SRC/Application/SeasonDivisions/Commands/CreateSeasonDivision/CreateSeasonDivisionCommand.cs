using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.SeasonDivisions.Commands.CreateSeasonDivision
{
    public class CreateSeasonDivisionCommand : IRequest
    {
        public int SeasonId { get; set; }
        public int DivisionId { get; set; }
        public int? WinnerSeasonDivisionTeamId { get; set; }


        public class Handler : IRequestHandler<CreateSeasonDivisionCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(CreateSeasonDivisionCommand request, CancellationToken cancellationToken)
            {
                var entity = new SeasonDivision
                {
                    SeasonId = request.SeasonId,
                    DivisionId = request.DivisionId,
                    WinnerSeasonDivisionTeamId = request.WinnerSeasonDivisionTeamId
                };

                _context.SeasonDivision.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
