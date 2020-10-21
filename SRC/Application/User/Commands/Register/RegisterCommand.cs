using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.User.Commands.Register
{
    public class RegisterCommand : IRequest<Dto.User>
    {

        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<RegisterCommand, Dto.User>
        {
            private readonly IBasketballLeagueDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(IBasketballLeagueDbContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<Dto.User> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if (await _context.AppUser.AnyAsync(x => x.Email == request.Email, cancellationToken))
                    throw new BadRequestException("Email already exist");

                if (await _context.AppUser.AnyAsync(x => x.UserName == request.UserName, cancellationToken))
                    throw new BadRequestException("Username already exist");

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.UserName,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                await _userManager.AddToRoleAsync(user, "USER");

                if (result.Succeeded)
                {
                    return new Dto.User
                    {
                        DisplayName = user.DisplayName,
                        Username = user.UserName,
                        Token = _jwtGenerator.CreateToken(user),
                        Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
                    };
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
