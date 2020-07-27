using Microsoft.AspNetCore.Identity;

namespace BasketballLeague.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
