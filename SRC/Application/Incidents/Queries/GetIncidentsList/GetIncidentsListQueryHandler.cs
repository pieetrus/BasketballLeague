﻿using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Incidents.Queries.GetIncidentsList
{
    public class GetIncidentsListQueryHandler : IRequestHandler<GetIncidentsListQuery, IEnumerable<Incident>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetIncidentsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Incident>> Handle(GetIncidentsListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Incident
                .Include(x => x.Shot).ThenInclude(x => x.Rebound)
                .Include(x => x.Shot).ThenInclude(x => x.Assist)
                .Include(x => x.Foul).ThenInclude(x => x.FreeThrows).ThenInclude(x => x.Assist)
                .Include(x => x.Foul).ThenInclude(x => x.FreeThrows).ThenInclude(x => x.Rebound)
                .Include(x => x.Turnover).ThenInclude(x => x.Steal)
                .Include(x => x.Substitution)
                .Include(x => x.Timeout)
                .AsQueryable();

            // todo add incident dto and shot dto

            if (request.MatchId.HasValue)
                queryable = queryable.Where(x => x.MatchId == request.MatchId);

            var incidents = await queryable.OrderByDescending(x => x.Quater).ThenBy(x => x.Minutes).ThenBy(x => x.Seconds).ToListAsync(cancellationToken);

            return incidents;
        }
    }
}