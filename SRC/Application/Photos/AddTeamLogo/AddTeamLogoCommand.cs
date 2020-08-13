using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Photos.AddTeamLogo
{
    public class AddTeamLogoCommand : IRequest<Photo>
    {
        public IFormFile File { get; set; }
        public int TeamId { get; set; }
    }

    public class Handler : IRequestHandler<AddTeamLogoCommand, Photo>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public Handler(IBasketballLeagueDbContext context, IPhotoAccessor photoAccessor)
        {
            _context = context;
            _photoAccessor = photoAccessor;
        }

        public async Task<Photo> Handle(AddTeamLogoCommand request, CancellationToken cancellationToken)
        {
            var photoUploadResult = _photoAccessor.AddPhoto(request.File);

            var team = await _context.Team
                .Include(x => x.Logo)
                .FirstOrDefaultAsync(x => x.Id == request.TeamId, cancellationToken);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                TeamId = request.TeamId
            };

            team.Logo = photo;

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return photo;

            throw new Exception("Problem saving changes");
        }
    }
}