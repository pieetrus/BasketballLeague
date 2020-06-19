using BasketballLeague.Application.Common.Interfaces;
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

            return services;
        }
    }
}
