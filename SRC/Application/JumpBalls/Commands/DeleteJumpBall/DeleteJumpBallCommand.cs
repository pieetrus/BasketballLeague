using MediatR;

namespace BasketballLeague.Application.JumpBalls.Commands.DeleteJumpBall
{
    public class DeleteJumpBallCommand : IRequest
    {
        public int Id { get; set; }
    }
}
