using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Commands.CreateDivision
{
    public class CreateDivisionCommand : IRequest
    {
        public string Name { get; set; }
        public string ShortName { get; set; }


        public class Handler : IRequestHandler<CreateDivisionCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
            {

                var entity = new Division
                {
                    Name = request.Name,
                    ShortName = request.ShortName
                };

                _context.Division.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }


    }
}
