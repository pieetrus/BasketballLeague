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
    public class GetDivisionListQueryHandler : IRequestHandler<GetDivisionListQuery, IEnumerable<Division>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetDivisionListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Division>> Handle(GetDivisionListQuery request, CancellationToken cancellationToken)
        {
            var divisions = await _context.Division.ToListAsync(cancellationToken);

            return divisions;
        }
    }
}
