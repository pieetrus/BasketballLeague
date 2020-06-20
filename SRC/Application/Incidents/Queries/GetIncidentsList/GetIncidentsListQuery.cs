using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Incidents.Queries.GetIncidentsList
{
    public class GetIncidentsListQuery : IRequest<IEnumerable<Incident>>
    {
    }
}
