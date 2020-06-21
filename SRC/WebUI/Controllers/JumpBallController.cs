using BasketballLeague.Application.JumpBalls.Commands.CreateJumpBall;
using BasketballLeague.Application.JumpBalls.Commands.DeleteJumpBall;
using BasketballLeague.Application.JumpBalls.Commands.UpdateJumpBall;
using BasketballLeague.Application.JumpBalls.Queries.GetJumpBallDetail;
using BasketballLeague.Application.JumpBalls.Queries.GetJumpBallsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class JumpBallController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<JumpBall>> GetAll()
        {
            return Ok(await Mediator.Send(new GetJumpBallsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JumpBall>> Get(int id)
        {
            var player = await Mediator.Send(new GetJumpBallDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateJumpBallCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJumpBallCommand command)
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
            await Mediator.Send(new DeleteJumpBallCommand { Id = id });

            return NoContent();
        }
    }
}