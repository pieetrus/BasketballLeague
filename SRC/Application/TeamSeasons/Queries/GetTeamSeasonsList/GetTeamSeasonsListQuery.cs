using BasketballLeague.Application.Teams;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonsListQuery : IRequest<IEnumerable<TeamDto>>
    {
        public GetTeamSeasonsListQuery(int? seasonId, int? divisionId)
        {
            SeasonId = seasonId;
            DivisionId = divisionId;
        }

        public int? SeasonId { get; set; }
        public int? DivisionId { get; set; }
    }
}
