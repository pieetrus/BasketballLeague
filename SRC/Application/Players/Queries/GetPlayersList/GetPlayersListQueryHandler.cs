﻿using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Players.Queries
{
    public class GetPlayersListQueryHandler : IRequestHandler<GetPlayersListQuery, IEnumerable<Player>>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayersListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Player>> Handle(GetPlayersListQuery request, CancellationToken cancellationToken)
        {
            var players = await _context.Player.ToListAsync(cancellationToken);

            return players;
        }
    }
}
