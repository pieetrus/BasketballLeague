using MediatR;

namespace BasketballLeague.Application.Coaches.Commands.DeleteCoach
{
    public class DeleteCoachCommand : IRequest
    {
        public int Id { get; set; }
    }
}
