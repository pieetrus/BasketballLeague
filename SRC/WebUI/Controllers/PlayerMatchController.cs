using BasketballLeague.Application.PlayerMatches.Commands.CreatePlayerMatch;
using BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchDetail;
using BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchesList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class PlayerMatchController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<PlayerMatch>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPlayerMatchesListQuery()));
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
        public async Task<IActionResult> Create([FromBody]CreatePlayerMatchCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}