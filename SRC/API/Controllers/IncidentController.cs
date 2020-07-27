using BasketballLeague.Application.Incidents.Queries.GetIncidentDetail;
using BasketballLeague.Application.Incidents.Queries.GetIncidentsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class IncidentController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Incident>> GetAll()
        {
            return Ok(await Mediator.Send(new GetIncidentsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Incident>> Get(int id)
        {
            var player = await Mediator.Send(new GetIncidentDetailQuery { Id = id });

            return Ok(player);
        }
    }
}