using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Teams.Queries.GetTeamDetail
{

    public class GetTeamDetailQuery : IRequest<Team>
    {
        public int Id { get; set; }
    }
}
