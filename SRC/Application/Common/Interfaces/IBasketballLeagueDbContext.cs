using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Common.Interfaces
{
    public interface IBasketballLeagueDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
