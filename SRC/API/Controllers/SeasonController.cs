using BasketballLeague.Application.Seasons;
using BasketballLeague.Application.Seasons.Commands.CreateSeason;
using BasketballLeague.Application.Seasons.Commands.DeleteSeason;
using BasketballLeague.Application.Seasons.Commands.UpdateSeason;
using BasketballLeague.Application.Seasons.Queries.GetSeasonDetail;
using BasketballLeague.Application.Seasons.Queries.GetSeasonsList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class SeasonController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetSeasonListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeasonDto>> Get(int id)
        {
            var player = await Mediator.Send(new GetSeasonDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateSeasonCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSeasonCommand command)
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
            await Mediator.Send(new DeleteSeasonCommand { Id = id });

            return NoContent();
        }
    }
}