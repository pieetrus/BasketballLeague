using BasketballLeague.Application.Turnovers.Commands.CreateTurnover;
using BasketballLeague.Application.Turnovers.Commands.DeleteTurnover;
using BasketballLeague.Application.Turnovers.Commands.UpdateTimeout;
using BasketballLeague.Application.Turnovers.Queries.GetTurnoverDetail;
using BasketballLeague.Application.Turnovers.Queries.GetTurnoverList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{

    public class TurnoverController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Turnover>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTurnoversListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Turnover>> Get(int id)
        {
            var player = await Mediator.Send(new GetTurnoverDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateTurnoverCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTurnoverCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTurnoverCommand { Id = id });

            return NoContent();
        }
    }
}