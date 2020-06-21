using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Substitutions.Queries.GetSubstitutionsList
{
    public class GetSubstitutionsListQueryHandler : IRequestHandler<GetSubstitutionsListQuery, IEnumerable<Substitution>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetSubstitutionsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Substitution>> Handle(GetSubstitutionsListQuery request, CancellationToken cancellationToken)
        {
            var substitution = await _context.Substitution.ToListAsync(cancellationToken);

            return substitution;
        }
    }
}