using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Incidents.Queries.GetIncidentsList
{
    public class GetIncidentsListQuery : IRequest<IEnumerable<Incident>>
    {
        public GetIncidentsListQuery(int? matchId)
        {
            MatchId = matchId;
        }

        public int? MatchId { get; set; }
    }
}
