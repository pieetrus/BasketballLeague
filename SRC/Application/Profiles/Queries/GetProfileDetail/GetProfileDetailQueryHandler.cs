using BasketballLeague.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Profiles.Queries.GetProfileDetail
{
    public class GetProfileDetailQueryHandler : IRequestHandler<GetProfileDetailQuery, Profile>
    {
        private readonly IBasketballLeagueDbContext _context;

        public GetProfileDetailQueryHandler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Profile> Handle(GetProfileDetailQuery request, CancellationToken cancellationToken)
        {

            var user = await _context.AppUser
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == request.Username, cancellationToken);

            return new Profile
            {
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                Photos = user.Photos,
                Bio = user.Bio
            };
        }
    }
}
