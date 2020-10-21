using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.User.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Dto.User>
    {
        public UserManager<AppUser> _userManager { get; }
        private readonly IBasketballLeagueDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginQueryHandler(IBasketballLeagueDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Dto.User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.AppUser
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(Dto.User), request.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            var roles = await _userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                return new Dto.User
                {
                    Username = user.UserName,
                    DisplayName = user.DisplayName,
                    Token = _jwtGenerator.CreateToken(user),
                    Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                    Role = roles[0]
                };
            }

            throw new UnauthorizedException(request.Email);
        }
    }
}
