using MediatR;

namespace BasketballLeague.Application.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
