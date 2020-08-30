using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionsList
{
    public class GetDivisionsListQueryHandler : IRequestHandler<GetDivisionsListQuery, IEnumerable<DivisionDto>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetDivisionsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DivisionDto>> Handle(GetDivisionsListQuery request, CancellationToken cancellationToken)
        {
            var divisions = await _context.Division.OrderBy(x => x.Level).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Division>, IEnumerable<DivisionDto>>(divisions);
        }
    }
}
