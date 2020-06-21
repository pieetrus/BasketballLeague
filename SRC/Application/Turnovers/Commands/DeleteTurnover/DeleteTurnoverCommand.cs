using MediatR;

namespace BasketballLeague.Application.Turnovers.Commands.DeleteTurnover
{
    public class DeleteTurnoverCommand : IRequest
    {
        public int Id { get; set; }
    }
}
