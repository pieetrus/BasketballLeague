﻿using BasketballLeague.Application.GetPlayerSeasons.Quieries.GetPlayerSeasonsList;
using BasketballLeague.Application.PlayerSeasons.Commands.CreatePlayerSeason;
using BasketballLeague.Application.PlayerSeasons.Commands.DeletePlayerSeason;
using BasketballLeague.Application.PlayerSeasons.Commands.UpdatePlayerSeason;
using BasketballLeague.Application.PlayerSeasons.Quieries.GetPlayerSeasonDetail;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{
    public class PlayerSeasonController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<PlayerSeason>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPlayerSeasonsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerSeason>> Get(int id)
        {
            var player = await Mediator.Send(new GetPlayerSeasonDetailQuery { Id = id });

            return Ok(player);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreatePlayerSeasonCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlayerSeasonCommand command)
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
            await Mediator.Send(new DeletePlayerSeasonCommand { Id = id });

            return NoContent();
        }
    }
}