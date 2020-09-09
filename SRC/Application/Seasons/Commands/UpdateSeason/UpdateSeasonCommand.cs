using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Seasons.Commands.UpdateSeason
{
    public class UpdateSeasonCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> DivisionsId { get; set; }


        public class Handler : IRequestHandler<UpdateSeasonCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
            {
                var newSeasonDivisions = new List<SeasonDivision>();

                var entity = await _context.Season
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Season), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.StartDate = request.StartDate ?? entity.StartDate;
                entity.EndDate = request.EndDate ?? entity.EndDate;

                if (request.DivisionsId.Any())
                {
                    var seasonDivisionsInDb = _context.SeasonDivision.Where(x => x.SeasonId == entity.Id).Select(x => x.DivisionId).ToList();

                    foreach (var divisionId in request.DivisionsId)
                    {
                        var seasonDivision = new SeasonDivision
                        {
                            SeasonId = entity.Id,
                            DivisionId = divisionId,
                        };


                        newSeasonDivisions.Add(seasonDivision);
                    }

                    newSeasonDivisions.RemoveAll(x => seasonDivisionsInDb.Contains(x.DivisionId));

                    _context.SeasonDivision.AddRange(newSeasonDivisions);

                }


                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
