using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.JumpBalls.Queries.GetJumpBallsList
{
    public class GetJumpBallsListQuery : IRequest<IEnumerable<JumpBall>>
    {
    }
}
