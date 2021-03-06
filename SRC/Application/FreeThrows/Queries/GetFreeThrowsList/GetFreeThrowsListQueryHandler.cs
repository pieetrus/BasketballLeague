﻿using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.FreeThrows.Queries.GetFreeThrowsList
{
    public class GetFreeThrowsListQueryHandler : IRequestHandler<GetFreeThrowsListQuery, IEnumerable<Domain.Entities.FreeThrows>>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetFreeThrowsListQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.FreeThrows>> Handle(GetFreeThrowsListQuery request, CancellationToken cancellationToken)
        {
            var freeThrows = await _context.FreeThrow.ToListAsync(cancellationToken);

            return freeThrows;
        }
    }
}