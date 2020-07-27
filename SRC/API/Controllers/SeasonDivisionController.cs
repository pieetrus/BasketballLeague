using BasketballLeague.Application.SeasonDivisions.Commands.CreateSeasonDivision;
using BasketballLeague.Application.SeasonDivisions.Commands.DeleteSeasonDivision;
using BasketballLeague.Application.SeasonDivisions.Commands.UpdateSeasonDivision;
using BasketballLeague.Application.SeasonDivisions.Queries.GetSeasonDivisionDetail;
using BasketballLeague.Application.SeasonDivisions.Queries.GetSeasonDivisionsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class SeasonDivisionController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<SeasonDivision>> GetAll()
        {
            return Ok(await Mediator.Send(new GetSeasonDivisionsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await Mediator.Send(new GetSeasonDivisionDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateSeasonDivisionCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSeasonDivisionCommand command)
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
            await Mediator.Send(new DeleteSeasonDivisionCommand { Id = id });

            return NoContent();
        }
    }
}