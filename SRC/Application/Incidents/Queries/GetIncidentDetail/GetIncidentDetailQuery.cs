using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Incidents.Queries.GetIncidentDetail
{
    public class GetIncidentDetailQuery : IRequest<Incident>
    {
        public int Id { get; set; }
    }
}
