using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.User.Commands.Register
{
    public class RegisterCommand : IRequest<AppUser>
    {

        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<RegisterCommand, AppUser>
        {
            private readonly IBasketballLeagueDbContext _context;
            private readonly UserManager<AppUser> _userManager;

            public Handler(IBasketballLeagueDbContext context, UserManager<AppUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<AppUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if(await _context.AppUser.AnyAsync(x => x.Email == request.Email, cancellationToken))
                    throw new BadRequestException("Email already exist");
                
                if (await _context.AppUser.AnyAsync(x => x.UserName == request.UserName, cancellationToken))
                    throw new BadRequestException("UserName already exist");

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new AppUser
                    {
                        DisplayName = user.DisplayName,
                        UserName = user.UserName,
                    };
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
