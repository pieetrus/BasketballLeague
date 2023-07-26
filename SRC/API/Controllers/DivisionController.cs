using BasketballLeague.Application.Divisions;
using BasketballLeague.Application.Divisions.Commands.CreateDivision;
using BasketballLeague.Application.Divisions.Commands.DeleteDivision;
using BasketballLeague.Application.Divisions.Commands.UpdateDivision;
using BasketballLeague.Application.Divisions.Queries.GetDivisionDetail;
using BasketballLeague.Application.Divisions.Queries.GetDivisionsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class DivisionController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetDivisionsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DivisionDto>> Get(int id)
        {
            var player = await Mediator.Send(new GetDivisionDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateDivisionCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
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