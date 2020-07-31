using BasketballLeague.Application.User.Commands.Register;
using BasketballLeague.Application.User.Dto;
using BasketballLeague.Application.User.Queries.GetCurrentUser;
using BasketballLeague.Application.User.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<User>> CurrentUser()
        {
            return await Mediator.Send(new GetCurrentUserQuery());
        }
    }
}
