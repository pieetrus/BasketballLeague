using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Coaches.Commands.CreateCoach
{
    public class CreateCoachCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }


        public class Handler : IRequestHandler<CreateCoachCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateCoachCommand request, CancellationToken cancellationToken)
            {
                var entity = new Coach
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Birthdate = request.Birthdate,
                    PhotoUrl = request.PhotoUrl
                };

                _context.Coach.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
