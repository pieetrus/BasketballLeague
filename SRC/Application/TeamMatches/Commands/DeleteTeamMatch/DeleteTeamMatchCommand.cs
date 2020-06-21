using MediatR;

namespace BasketballLeague.Application.TeamMatches.Commands.DeleteTeamMatch
{
    public class DeleteTeamMatchCommand : IRequest
    {
        public int Id { get; set; }
    }
}
