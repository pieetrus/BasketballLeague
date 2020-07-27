using BasketballLeague.Application.Shots.Commands.CreateShot;
using BasketballLeague.Application.Shots.Commands.DeleteShot;
using BasketballLeague.Application.Shots.Commands.UpdateShot;
using BasketballLeague.Application.Shots.Queries.GetShotDetail;
using BasketballLeague.Application.Shots.Queries.GetShotsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class ShotController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Shot>> GetAll()
        {
            return Ok(await Mediator.Send(new GetShotsListQuery()));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await Mediator.Send(new GetShotDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateShotCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateShotCommand command)
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
            await Mediator.Send(new DeleteShotCommand { Id = id });

            return NoContent();
        }
    }
}