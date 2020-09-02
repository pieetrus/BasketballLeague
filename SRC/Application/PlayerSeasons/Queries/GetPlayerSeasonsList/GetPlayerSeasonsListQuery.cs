using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonsList
{
    public class GetPlayerSeasonsListQuery : IRequest<IEnumerable<PlayerSeasonListDto>>
    {
        public GetPlayerSeasonsListQuery(int? seasonId, int? divisionId, int? teamId)
        {
            SeasonId = seasonId;
            DivisionId = divisionId;
            TeamId = teamId;
        }
        public int? SeasonId { get; set; }
        public int? DivisionId { get; set; }
        public int? TeamId { get; set; }
    }
}
