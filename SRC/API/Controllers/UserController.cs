using BasketballLeague.Application.User.Commands.Register;
using BasketballLeague.Application.User.Queries.Login;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<Application.User.Dto.User>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
