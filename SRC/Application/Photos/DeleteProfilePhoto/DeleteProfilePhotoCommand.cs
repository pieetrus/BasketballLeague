using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Photos.DeleteProfilePhoto
{

    public class DeleteProfilePhotoCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<DeleteProfilePhotoCommand, Unit>
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

        public async Task<Unit> Handle(DeleteProfilePhotoCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.AppUser
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(
                    x => x.UserName == _userAccessor.GetCurrentUsername(), cancellationToken);

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

            if (photo == null)
                throw new NotFoundException(nameof(Photo), request.Id);

            if (photo.IsMain)
                throw new BadRequestException("You cannot delete your main photo");

            var result = _photoAccessor.DeletePhoto(request.Id);

            if (result == null)
                throw new Exception("Problem deleting the photo");

            user.Photos.Remove(photo);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
