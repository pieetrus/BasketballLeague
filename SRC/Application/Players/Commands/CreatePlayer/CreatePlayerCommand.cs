using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Position = BasketballLeague.Domain.Common.Postition;

namespace BasketballLeague.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }
        public byte? Height { get; set; }
        public Position Position { get; set; }


        public class Handler : IRequestHandler<CreatePlayerCommand, int>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context, IMapper mapper)
            {
                _context = context;
            }

            public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Player
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Birthdate = request.Birthdate,
                    PhotoUrl = request.PhotoUrl,
                    Height = request.Height,
                    Position = request.Position
                };

                _context.Player.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.PlayerId;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
