using BasketballLeague.Application.Divisions.Commands.CreateDivision;
using BasketballLeague.Application.Divisions.Commands.DeleteDivision;
using BasketballLeague.Application.Divisions.Commands.UpdateDivision;
using BasketballLeague.Application.Divisions.Queries.GetDivisionDetail;
using BasketballLeague.Application.Divisions.Queries.GetDivisionsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class DivisionController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Division>> GetAll()
        {
            return Ok(await Mediator.Send(new GetDivisionListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Division>> Get(int id)
        {
            var player = await Mediator.Send(new GetDivisionDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateDivisionCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDivisionCommand command)
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
            await Mediator.Send(new DeleteDivisionCommand { Id = id });

            return NoContent();
        }
    }
}