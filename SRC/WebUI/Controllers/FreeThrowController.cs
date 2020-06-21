using BasketballLeague.Application.FreeThrows.Commands.CreateFreeThrow;
using BasketballLeague.Application.FreeThrows.Commands.DeleteFreeThrow;
using BasketballLeague.Application.FreeThrows.Commands.UpdateFreeThrow;
using BasketballLeague.Application.FreeThrows.Queries.GetFreeThrowDetail;
using BasketballLeague.Application.FreeThrows.Queries.GetFreeThrowsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class FreeThrowController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<FreeThrow>> GetAll()
        {
            return Ok(await Mediator.Send(new GetFreeThrowsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FreeThrow>> Get(int id)
        {
            var player = await Mediator.Send(new GetFreeThrowDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateFreeThrowCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFreeThrowCommand command)
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
            await Mediator.Send(new DeleteFreeThrowCommand { Id = id });

            return NoContent();
        }
    }
}