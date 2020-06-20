using MediatR;

namespace BasketballLeague.Application.Referees.Commands.DeleteReferee
{
    public class DeleteRefereeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
