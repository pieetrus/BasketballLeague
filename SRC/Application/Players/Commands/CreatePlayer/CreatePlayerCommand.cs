using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhotoUrl { get; set; }
        public byte? Height { get; set; }
        public int? Position { get; set; }


        public class Handler : IRequestHandler<CreatePlayerCommand>
        {
            private readonly IBasketballLeagueDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IBasketballLeagueDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Player
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Birthdate = request.Birthdate,
                    PhotoUrl = request.PhotoUrl,
                    Height = request.Height,
                    Position = (BasketballLeague.Domain.Common.Postition)request.Position
                };

                _context.Player.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
