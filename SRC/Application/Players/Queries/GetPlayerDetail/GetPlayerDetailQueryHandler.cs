using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Players.Queries.GetPlayerDetail
{
    public class GetPlayerDetailQueryHandler : IRequestHandler<GetPlayerDetailQuery, Player>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerDetailQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Player> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Player.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            return entity;
        }
    }

}
