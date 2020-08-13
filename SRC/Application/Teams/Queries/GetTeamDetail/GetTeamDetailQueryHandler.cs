using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Teams.Queries.GetTeamDetail
{
    public class GetTeamDetailQueryHandler : IRequestHandler<GetTeamDetailQuery, TeamDto>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamDetailQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TeamDto> Handle(GetTeamDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Team
                .Include(x => x.Logo)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            return _mapper.Map<Team, TeamDto>(entity);
        }
    }
}
