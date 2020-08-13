using BasketballLeague.Application.Photos.AddProfilePhoto;
using BasketballLeague.Application.Photos.AddTeamLogo;
using BasketballLeague.Application.Photos.DeleteProfilePhoto;
using BasketballLeague.Application.Photos.SetMainProfilePhoto;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class PhotoController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Photo>> AddProfilePhoto([FromForm] AddProfilePhotoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("team/{id}")]
        public async Task<ActionResult<Photo>> AddTeamLogo(int id, [FromForm] AddTeamLogoCommand command)
        {
            command.TeamId = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            return await Mediator.Send(new DeleteProfilePhotoCommand { Id = id });
        }

        [HttpPost("{id}/setmain")]
        public async Task<ActionResult<Unit>> SetMain(string id)
        {
            return await Mediator.Send(new SetMainProfilePhotoCommand { Id = id });
        }
    }
}


