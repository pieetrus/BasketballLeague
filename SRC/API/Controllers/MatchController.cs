﻿using BasketballLeague.Application.Matches.Commands.CreateMatch;
using BasketballLeague.Application.Matches.Commands.DeleteMatch;
using BasketballLeague.Application.Matches.Commands.EndMatch;
using BasketballLeague.Application.Matches.Commands.StartMatch;
using BasketballLeague.Application.Matches.Commands.UpdateMatch;
using BasketballLeague.Application.Matches.Queries.GetDetailedMatchesList;
using BasketballLeague.Application.Matches.Queries.GetMatchDetail;
using BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed;
using BasketballLeague.Application.Matches.Queries.GetMatchesList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class MatchController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Match>> GetAll()
        {
            return Ok(await Mediator.Send(new GetMatchesListQuery()));
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<MatchListDto>> GetAllDetailed()
        {
            return Ok(await Mediator.Send(new GetDetailedMatchesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Match>> Get(int id)
        {
            var player = await Mediator.Send(new GetMatchDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpGet("detailed/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchDetailedDto>> GetDetailed(int id)
        {
            var player = await Mediator.Send(new GetMatchDetailDetailedQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateMatchCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> StartMatch([FromBody] StartMatchCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("end")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EndMatch([FromBody] EndMatchCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMatchCommand command)
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
            await Mediator.Send(new DeleteMatchCommand { Id = id });

            return NoContent();
        }
    }
}