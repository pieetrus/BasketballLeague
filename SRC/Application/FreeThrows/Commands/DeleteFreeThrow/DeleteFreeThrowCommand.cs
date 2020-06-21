using MediatR;

namespace BasketballLeague.Application.FreeThrows.Commands.DeleteFreeThrow
{
    public class DeleteFreeThrowCommand : IRequest
    {
        public int Id { get; set; }
    }
}
