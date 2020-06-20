using BasketballLeague.Application.Players.Queries;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{

    public class PlayerController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Player>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPlayersListQuery()));
        }
    }
}