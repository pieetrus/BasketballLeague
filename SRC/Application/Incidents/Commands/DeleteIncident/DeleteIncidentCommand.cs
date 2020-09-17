using MediatR;

namespace BasketballLeague.Application.Incidents.Commands.DeleteIncident
{
    public class DeleteIncidentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
