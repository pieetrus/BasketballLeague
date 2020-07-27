using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketballLeague.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketballLeagueDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BasketballLeagueDatabase")));

            services.AddScoped<IBasketballLeagueDbContext>(provider => provider.GetService<BasketballLeagueDbContext>());

            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<BasketballLeagueDbContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            return services;
        }
    }
}
