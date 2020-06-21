using AutoMapper;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.JumpBalls.Queries.GetJumpBallsList
{
    public class GetJumpBallsListQueryHandler : IRequestHandler<GetJumpBallsListQuery, IEnumerable<JumpBall>>
    {

        private readonly IBasketballLeagueDbContext _context;
        private readonly IMapper _mapper;

        public GetJumpBallsListQueryHandler(IBasketballLeagueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JumpBall>> Handle(GetJumpBallsListQuery request, CancellationToken cancellationToken)
        {
            var jumpBalls = await _context.JumpBall.ToListAsync(cancellationToken);

            return jumpBalls;
        }
    }
}