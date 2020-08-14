using AutoMapper;
using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionDetail
{
    public class GetDivisionDetailQueryHandler : IRequestHandler<GetDivisionDetailQuery, DivisionDto>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetDivisionDetailQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DivisionDto> Handle(GetDivisionDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Division.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Division), request.Id);
            }

            return _mapper.Map<Division, DivisionDto>(entity); ;
        }
    }
}
