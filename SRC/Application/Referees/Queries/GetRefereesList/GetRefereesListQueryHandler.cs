using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Referees.Queries.GetRefereesList
{
    public class GetRefereesListQueryHandler : IRequestHandler<GetRefereesListQuery, IEnumerable<Referee>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetRefereesListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Referee>> Handle(GetRefereesListQuery request, CancellationToken cancellationToken)
        {
            var referees = await _context.Referee.ToListAsync(cancellationToken);

            return referees;
        }
    }
}
