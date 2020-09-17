using BasketballLeague.Application.Timeouts.Commands.CreateTimeout;
using BasketballLeague.Application.Timeouts.Commands.DeleteTimeout;
using BasketballLeague.Application.Timeouts.Commands.UpdateTimeout;
using BasketballLeague.Application.Timeouts.Queries.GetTimeoutDetail;
using BasketballLeague.Application.Timeouts.Queries.GetTimeoutsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{

    public class TimeoutController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Timeout>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTimeoutsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Substitution>> Get(int id)
        {
            var player = await Mediator.Send(new GetTimeoutDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateTimeoutCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTimeoutCommand command)
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
            await Mediator.Send(new DeleteTimeoutCommand { Id = id });

            return NoContent();
        }
    }
}