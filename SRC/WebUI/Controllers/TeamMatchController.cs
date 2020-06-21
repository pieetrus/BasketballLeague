using BasketballLeague.Application.TeamMatches.Commands.CreateTeamMatch;
using BasketballLeague.Application.TeamMatches.Commands.DeleteTeamMatch;
using BasketballLeague.Application.TeamMatches.Commands.UpdateTeamMatch;
using BasketballLeague.Application.TeamMatches.Queries.GetTeamMatchDetail;
using BasketballLeague.Application.TeamMatches.Queries.GetTeamMatchesList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class TeamMatchController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<TeamMatch>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTeamMatchesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamMatch>> Get(int id)
        {
            var player = await Mediator.Send(new GetTeamMatchDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateTeamMatchCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamMatchCommand command)
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
            await Mediator.Send(new DeleteTeamMatchCommand { Id = id });

            return NoContent();
        }
    }
}