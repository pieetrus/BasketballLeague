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
        private readonly IBasketballLeagueDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginQueryHandler(IBasketballLeagueDbContext context, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
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

            if (result.Succeeded)
            {
                return new Dto.User
                {
                    Username = user.UserName,
                    DisplayName = user.DisplayName,
                    Token = _jwtGenerator.CreateToken(user),
                    Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
                };
            }

            throw new UnauthorizedException(request.Email);
        }
    }
}
