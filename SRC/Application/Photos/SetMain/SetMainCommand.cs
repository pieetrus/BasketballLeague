using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Photos.SetMain
{
    public class SetMainCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<SetMainCommand, Unit>
    {
        private readonly IBasketballLeagueDbContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(IBasketballLeagueDbContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(SetMainCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.AppUser
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(
                x => x.UserName == _userAccessor.GetCurrentUsername(), cancellationToken);

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

            if (photo == null)
                throw new NotFoundException(nameof(Photo), request.Id);

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain == null)
                throw new NotFoundException(nameof(Photo), user.Id + " main photo");

            if (photo.Id == currentMain.Id)
                throw new Exception("Photo is already your main photo");

            currentMain.IsMain = false;
            photo.IsMain = true;

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
