using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var incidents = await _context.Incident.ToListAsync(cancellationToken);

            return incidents;
        }
    }
}