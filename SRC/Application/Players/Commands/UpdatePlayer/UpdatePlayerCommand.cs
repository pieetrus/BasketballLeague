using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }
        public byte? Height { get; set; }
        public byte? Position { get; set; }

        public class Handler : IRequestHandler<UpdatePlayerCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Player
                    .SingleOrDefaultAsync(x => x.PlayerId == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Player), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.Surname = request.Surname ?? entity.Surname;
                entity.Birthdate = request.Birthdate ?? entity.Birthdate;
                entity.PhotoUrl = request.PhotoUrl ?? entity.PhotoUrl;
                entity.Height = request.Height ?? entity.Height;
                entity.Position = request.Position ?? entity.Position;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
