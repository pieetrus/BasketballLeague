using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.JumpBalls.Queries.GetJumpBallDetail
{
    public class GetJumpBallDetailQuery : IRequest<JumpBall>
    {
        public int Id { get; set; }
    }
}