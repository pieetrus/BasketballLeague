using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.SeasonDivisions.Commands.UpdateSeasonDivision
{
    public class UpdateSeasonDivisionCommand : IRequest
    {
        public int Id { get; set; }
        public int? SeasonId { get; set; }
        public int? DivisionId { get; set; }
        public int? WinnerSeasonDivisionTeamId { get; set; }

        public class Handler : IRequestHandler<UpdateSeasonDivisionCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateSeasonDivisionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.SeasonDivision
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(SeasonDivision), request.Id);
                }

                entity.SeasonId = request.SeasonId ?? entity.SeasonId;
                entity.DivisionId = request.DivisionId ?? entity.DivisionId;
                entity.WinnerSeasonDivisionTeamId = request.WinnerSeasonDivisionTeamId ?? entity.WinnerSeasonDivisionTeamId;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}

