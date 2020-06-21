using MediatR;

namespace BasketballLeague.Application.PlayerMatches.Commands.DeletePlayerMatch
{
    public class DeletePlayerMatchCommand : IRequest
    {
        public int Id { get; set; }
    }
}
