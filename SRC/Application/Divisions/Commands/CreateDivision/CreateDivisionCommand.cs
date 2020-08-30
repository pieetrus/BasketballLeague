using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Commands.CreateDivision
{
    public class CreateDivisionCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Level { get; set; }


        public class Handler : IRequestHandler<CreateDivisionCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
            {

                var entity = new Division
                {
                    Name = request.Name,
                    ShortName = request.ShortName,
                    Level = request.Level

                };

                _context.Division.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }


    }
}
