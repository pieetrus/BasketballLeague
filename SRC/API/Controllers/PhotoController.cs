using BasketballLeague.Application.Photos.AddPhoto;
using BasketballLeague.Application.Photos.DeletePhoto;
using BasketballLeague.Application.Photos.SetMain;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.API.Controllers
{
    public class PhotoController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Photo>> Add([FromForm] AddPhotoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            return await Mediator.Send(new DeletePhotoCommand { Id = id });
        }

        [HttpPost("{id}/setmain")]
        public async Task<ActionResult<Unit>> SetMain(string id)
        {
            return await Mediator.Send(new SetMainCommand { Id = id });
        }
    }
}


