using MediatR;

namespace BasketballLeague.Application.Teams.Queries.GetTeamDetail
{

    public class GetTeamDetailQuery : IRequest<TeamDto>
    {
        public int Id { get; set; }
    }
}
