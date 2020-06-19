using BasketballLeague.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasketballLeague.Persistence
{
    public class BasketballLeagueDbContext : DbContext, IBasketballLeagueDbContext
    {

        public BasketballLeagueDbContext(DbContextOptions options) : base(options)
        {

        }





    }
}
