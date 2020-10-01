using BasketballLeague.Application.Teams;
using MediatR;
using System;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsListDto
{
    public class GetTeamSeasonsListQueryDto : IRequest<IEnumerable<TeamDto>>
    {
        public GetTeamSeasonsListQueryDto(int? seasonId, int? divisionId, DateTime? matchStartDate)
        {
            SeasonId = seasonId;
            DivisionId = divisionId;
            MatchStartDate = matchStartDate;
        }

        public int? SeasonId { get; set; }
        public int? DivisionId { get; set; }
        public DateTime? MatchStartDate { get; set; }
    }
}
