using MediatR;

namespace BasketballLeague.Application.Shots.Commands.DeleteShot
{
    public class DeleteShotCommand : IRequest
    {
        public int Id { get; set; }
    }
}
