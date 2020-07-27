using BasketballLeague.Domain.Entities;

namespace BasketballLeague.Application.Common.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);

    }
}
