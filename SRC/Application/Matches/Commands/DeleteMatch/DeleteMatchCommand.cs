using MediatR;

namespace BasketballLeague.Application.Matches.Commands.DeleteMatch
{
    public class DeleteMatchCommand : IRequest
    {
        public int Id { get; set; }
    }
}
