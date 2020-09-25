using BasketballLeague.Application.PlayerMatches;
using BasketballLeague.Application.PlayerMatches.Commands.CreatePlayerMatch;
using BasketballLeague.Application.PlayerMatches.Commands.DeletePlayerMatch;
using BasketballLeague.Application.PlayerMatches.Commands.UpdatePlayerMatch;
using BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchDetail;
using BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchesList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class PlayerMatchController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerMatchDto>>> GetAll(int? matchId)
        {
            return Ok(await Mediator.Send(new GetPlayerMatchesListQuery(matchId)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Division>> Get(int id)
        {
            var player = await Mediator.Send(new GetPlayerMatchDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreatePlayerMatchCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlayerMatchCommand command)
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
            await Mediator.Send(new DeletePlayerMatchCommand { Id = id });

            return NoContent();
        }
    }
}