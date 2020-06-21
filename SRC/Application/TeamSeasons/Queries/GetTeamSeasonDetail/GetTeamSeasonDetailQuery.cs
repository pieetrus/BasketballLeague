using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonDetail
{
    public class GetTeamSeasonDetailQuery : IRequest<TeamSeason>
    {
        public int Id { get; set; }
    }
}
