using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.TeamMatches.Queries.GetTeamMatchDetail
{
    public class GetTeamMatchDetailQuery : IRequest<TeamMatch>
    {
        public int Id { get; set; }
    }
}
