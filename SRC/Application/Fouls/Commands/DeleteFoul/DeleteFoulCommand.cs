using MediatR;

namespace BasketballLeague.Application.Fouls.Commands.DeleteFoul
{
    public class DeleteFoulCommand : IRequest
    {
        public int Id { get; set; }
    }
}
