using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonListQuery : IRequest<IEnumerable<TeamSeasonListDto>>
    {

        public GetTeamSeasonListQuery(int? teamId, int? seasonId, int? divisionId)
        {
            TeamId = teamId;
            SeasonId = seasonId;
            DivisionId = divisionId;
        }
        public int? TeamId { get; }
        public int? SeasonId { get; }
        public int? DivisionId { get; }
    }
}
