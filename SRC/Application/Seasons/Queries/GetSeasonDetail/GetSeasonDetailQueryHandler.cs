using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Seasons.Queries.GetSeasonDetail
{
    public class GetSeasonDetailQueryHandler : IRequestHandler<GetSeasonDetailQuery, SeasonDto>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonDetailQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SeasonDto> Handle(GetSeasonDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Season
                .Include(x => x.SeasonDivisions).ThenInclude(x => x.Division)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Season), request.Id);

            return _mapper.Map<Season, SeasonDto>(entity);
        }
    }
}