using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Coaches.Commands.UpdateCoach
{
    public class UpdateCoachCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }

        public class Handler : IRequestHandler<UpdateCoachCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCoachCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Coach.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Coach), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.Surname = request.Surname ?? entity.Surname;
                entity.Birthdate = request.Birthdate ?? entity.Birthdate;
                entity.PhotoUrl = request.PhotoUrl ?? entity.PhotoUrl;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
