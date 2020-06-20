using MediatR;

namespace BasketballLeague.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : IRequest
    {
        public int Id { get; set; }
    }
}
