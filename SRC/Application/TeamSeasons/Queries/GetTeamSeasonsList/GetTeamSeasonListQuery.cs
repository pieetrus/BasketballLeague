using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonListQuery : IRequest<IEnumerable<TeamSeasonListDto>>
    {

        public GetTeamSeasonListQuery(int? teamId)
        {
            TeamId = teamId;
        }
        public int? TeamId { get; }
    }
}
