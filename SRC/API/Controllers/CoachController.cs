using BasketballLeague.Application.Coaches.Commands.CreateCoach;
using BasketballLeague.Application.Coaches.Commands.DeleteCoach;
using BasketballLeague.Application.Coaches.Commands.UpdateCoach;
using BasketballLeague.Application.Coaches.Queries.GetCoachDetail;
using BasketballLeague.Application.Coaches.Queries.GetCoachesList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class CoachController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Coach>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCoachesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Coach>> Get(int id)
        {
            var player = await Mediator.Send(new GetCoachDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateCoachCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCoachCommand command)
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
            await Mediator.Send(new DeleteCoachCommand { Id = id });

            return NoContent();
        }
    }
}