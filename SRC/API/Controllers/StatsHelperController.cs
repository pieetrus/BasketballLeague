using BasketballLeague.Application.PlayerMatches;
using BasketballLeague.Application.StatsHelpers.Queries.GetPlayerBeforeMatchByPlayerSeasonList;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class StatsHelperController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<PlayerBeforeMatchDto>>> GetAll([FromQuery(Name = "Id")] List<int> Id)
        {
            return Ok(await Mediator.Send(new GetPlayerBeforeMatchByPlayerSeasonListQuery { Id = Id }));
        }
    }
}
