using BasketballLeague.Application.Referees.Commands.CreateReferee;
using BasketballLeague.Application.Referees.Commands.DeleteReferee;
using BasketballLeague.Application.Referees.Commands.UpdateReferee;
using BasketballLeague.Application.Referees.Queries.GetRefereeDetail;
using BasketballLeague.Application.Referees.Queries.GetRefereesList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class RefereeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Referee>> GetAll()
        {
            return Ok(await Mediator.Send(new GetRefereesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Referee>> Get(int id)
        {
            var player = await Mediator.Send(new GetRefereeDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateRefereeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRefereeCommand command)
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
            await Mediator.Send(new DeleteRefereeCommand { Id = id });

            return NoContent();
        }
    }
}