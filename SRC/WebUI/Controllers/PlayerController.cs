using BasketballLeague.Application.Players.Commands.CreatePlayer;
using BasketballLeague.Application.Players.Commands.DeletePlayer;
using BasketballLeague.Application.Players.Commands.UpdatePlayer;
using BasketballLeague.Application.Players.Queries;
using BasketballLeague.Application.Players.Queries.GetPlayerDetail;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{

    public class PlayerController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Player>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPlayersListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await Mediator.Send(new GetPlayerDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreatePlayerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdatePlayerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePlayerCommand { Id = id });

            return NoContent();
        }
    }
}