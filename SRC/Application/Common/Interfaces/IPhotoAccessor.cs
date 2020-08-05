using BasketballLeague.Application.Photos;
using Microsoft.AspNetCore.Http;

namespace BasketballLeague.Application.Common.Interfaces
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);
        string DeletePhoto(string publicId);
    }
}
