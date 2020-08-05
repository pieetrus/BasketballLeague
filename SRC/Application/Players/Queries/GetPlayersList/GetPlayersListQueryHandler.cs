using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Players.Queries.GetPlayersList
{
    public class GetPlayersListQueryHandler : IRequestHandler<GetPlayersListQuery, IEnumerable<PlayerListDto>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayersListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerListDto>> Handle(GetPlayersListQuery request, CancellationToken cancellationToken)
        {
            var players = await _context.Player
                .Include(x => x.PlayerSeasons)
                .ThenInclude(x => x.Team.Team)
                .OrderBy(x => x.Surname.ToUpper())
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Player>, IEnumerable<PlayerListDto>>(players).ToList();
        }
    }
}

