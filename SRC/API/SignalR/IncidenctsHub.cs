using BasketballLeague.Application.Shots.Commands.CreateShot;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BasketballLeague.API.SignalR
{
    public class IncidenctsHub : Hub
    {
        public IMediator _mediator { get; }

        public IncidenctsHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendIncident(CreateShotCommand command)
        {
            var incident = await _mediator.Send(command);

            await Clients.All.SendAsync("ReceiveIncident", "success");
        }

    }
}
