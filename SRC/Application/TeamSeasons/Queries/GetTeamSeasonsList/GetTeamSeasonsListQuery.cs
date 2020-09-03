using BasketballLeague.Application.Teams;
using MediatR;
using System;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonsListQuery : IRequest<IEnumerable<TeamDto>>
    {
        public GetTeamSeasonsListQuery(int? seasonId, int? divisionId, DateTime? matchStartDate)
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
