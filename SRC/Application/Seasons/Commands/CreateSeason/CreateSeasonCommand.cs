using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommand : IRequest<int>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> DivisionsId { get; set; }


        public class Handler : IRequestHandler<CreateSeasonCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
            {

                var newSeasonDivisions = new List<SeasonDivision>();

                var season = new Season
                {
                    Name = request.Name,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                foreach (var divisionId in request.DivisionsId)
                {
                    var seasonDivision = new SeasonDivision
                    {
                        Season = season,
                        DivisionId = divisionId,
                    };

                    newSeasonDivisions.Add(seasonDivision);
                }

                _context.SeasonDivision.AddRange(newSeasonDivisions);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return season.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
