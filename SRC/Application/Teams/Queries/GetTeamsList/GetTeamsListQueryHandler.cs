using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Teams.Queries.GetTeamsList
{
    public class GetTeamsListQueryHandler : IRequestHandler<GetTeamsListQuery, IEnumerable<Team>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Team>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
        {
            var teams = await _context.Team.ToListAsync(cancellationToken);

            return teams;
        }
    }
}
