using BasketballLeague.Application.Teams;
using BasketballLeague.Application.TeamSeasons.Commands.CreateTeamSeason;
using BasketballLeague.Application.TeamSeasons.Commands.DeleteTeamSeason;
using BasketballLeague.Application.TeamSeasons.Commands.UpdateTeamSeason;
using BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonDetail;
using BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList;
using BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsListDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class TeamSeason : BaseController
    {
        [HttpGet("dto")]
        public async Task<ActionResult<TeamDto>> GetAll(int? seasonId, int? divisionId, DateTime? matchStartDate)
        {
            return Ok(await Mediator.Send(new GetTeamSeasonsListQueryDto(seasonId, divisionId, matchStartDate)));
        }

        [HttpGet]
        public async Task<ActionResult<TeamSeasonListDto>> GetAll(int? teamId)
        {
            return Ok(await Mediator.Send(new GetTeamSeasonListQuery(teamId)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamSeason>> Get(int id)
        {
            var player = await Mediator.Send(new GetTeamSeasonDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateTeamSeasonCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamSeasonCommand command)
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
            await Mediator.Send(new DeleteTeamSeasonCommand { Id = id });

            return NoContent();
        }
    }
}