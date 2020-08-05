using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Photos.AddPhoto
{
    public class AddPhotoCommand : IRequest<Photo>
    {
        public IFormFile File { get; set; }
    }

    public class Handler : IRequestHandler<AddPhotoCommand, Photo>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IPhotoAccessor _photoAccessor;

        public Handler(IBasketballLeagueDbContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }

        public async Task<Photo> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
        {
            var photoUploadResult = _photoAccessor.AddPhoto(request.File);

            var user = await _context.AppUser
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(
                x => x.UserName == _userAccessor.GetCurrentUsername(), cancellationToken);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId
            };

            if (!user.Photos.Any(x => x.IsMain))
                photo.IsMain = true;

            user.Photos.Add(photo);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return photo;

            throw new Exception("Problem saving changes");
        }
    }
}
