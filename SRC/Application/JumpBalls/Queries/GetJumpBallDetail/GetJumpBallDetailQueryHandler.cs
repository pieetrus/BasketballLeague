using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.JumpBalls.Queries.GetJumpBallDetail
{
    public class GetJumpBallDetailQueryHandler : IRequestHandler<GetJumpBallDetailQuery, JumpBall>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetJumpBallDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<JumpBall> Handle(GetJumpBallDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JumpBall
                .Include(x => x.Incident)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JumpBall), request.Id);
            }

            return entity;
        }
    }
}
