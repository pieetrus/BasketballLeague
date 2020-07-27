using BasketballLeague.Application.Teams.Commands.CreateTeam;
using BasketballLeague.Application.Teams.Commands.DeleteTeam;
using BasketballLeague.Application.Teams.Commands.UpdateTeam;
using BasketballLeague.Application.Teams.Queries.GetTeamDetail;
using BasketballLeague.Application.Teams.Queries.GetTeamsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class TeamController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<Team>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTeamsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await Mediator.Send(new GetTeamDetailQuery{ Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateTeamCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamCommand command)
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
            await Mediator.Send(new DeleteTeamCommand { Id = id });

            return NoContent();
        }
    }
}