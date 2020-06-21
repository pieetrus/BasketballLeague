using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.GetPlayerSeasons.Quieries.GetPlayerSeasonsList
{
    public class GetPlayerSeasonsListQueryHandler : IRequestHandler<GetPlayerSeasonsListQuery, IEnumerable<PlayerSeason>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerSeasonsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerSeason>> Handle(GetPlayerSeasonsListQuery request, CancellationToken cancellationToken)
        {
            var playerSeasons = await _context.PlayerSeason.ToListAsync(cancellationToken);

            return playerSeasons;
        }
    }
}