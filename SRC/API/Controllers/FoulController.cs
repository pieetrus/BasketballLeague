using BasketballLeague.Application.Fouls.Commands.CreateFoul;
using BasketballLeague.Application.Fouls.Commands.DeleteFoul;
using BasketballLeague.Application.Fouls.Commands.UpdateFoul;
using BasketballLeague.Application.Fouls.Queries.GetFoulDetail;
using BasketballLeague.Application.Fouls.Queries.GetFoulsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{

    public class FoulController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Foul>> GetAll()
        {
            return Ok(await Mediator.Send(new GetFoulsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Division>> Get(int id)
        {
            var player = await Mediator.Send(new GetFoulDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateFoulCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFoulCommand command)
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
            await Mediator.Send(new DeleteFoulCommand { Id = id });

            return NoContent();
        }
    }
}