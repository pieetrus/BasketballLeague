using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Teams.Queries.GetTeamsList
{
    public class GetTeamsListQueryHandler : IRequestHandler<GetTeamsListQuery, IEnumerable<TeamDto>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
        {
            var teams = await _context.Team.Include(x => x.Logo).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Team>, IEnumerable<TeamDto>>(teams).ToList();
        }
    }
}
