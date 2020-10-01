using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Players.Queries.GetPlayerDetail
{
    public class GetPlayerDetailQueryHandler : IRequestHandler<GetPlayerDetailQuery, GetPlayerDetailQueryHandler.PlayerDto>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerDetailQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayerDto> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Player.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            return _mapper.Map<Player, PlayerDto>(entity);
        }

        public class PlayerDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime? Birthdate { get; set; }
            public string PhotoUrl { get; set; }
            public int? Height { get; set; }
            public string Position { get; set; }
        }
    }

}
