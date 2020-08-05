using BasketballLeague.Application.Profiles;
using BasketballLeague.Application.Profiles.Queries.GetProfileDetail;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class ProfileController : BaseController
    {
        [HttpGet("{username}")]
        public async Task<ActionResult<Profile>> Get(string username)
        {
            return await Mediator.Send(new GetProfileDetailQuery{Username = username});
        }
    }
}
