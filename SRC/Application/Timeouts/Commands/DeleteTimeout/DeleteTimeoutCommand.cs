using MediatR;

namespace BasketballLeague.Application.Timeouts.Commands.DeleteTimeout
{
    public class DeleteTimeoutCommand : IRequest
    {
        public int Id { get; set; }
    }
}
