using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.User.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Dto.User>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserAccessor _userAccessor;

        public GetCurrentUserQueryHandler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _userAccessor = userAccessor;
        }
        public async Task<Dto.User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

            return new Dto.User
            {
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Token = _jwtGenerator.CreateToken(user),
                Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
        }
    }
}
