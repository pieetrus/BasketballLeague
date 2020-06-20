using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionsList
{
    public class GetDivisionsListQueryHandler : IRequestHandler<GetDivisionsListQuery, IEnumerable<Division>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetDivisionsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Division>> Handle(GetDivisionsListQuery request, CancellationToken cancellationToken)
        {
            var divisions = await _context.Division.ToListAsync(cancellationToken);

            return divisions;
        }
    }
}
