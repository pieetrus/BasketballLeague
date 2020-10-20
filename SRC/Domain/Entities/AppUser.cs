using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BasketballLeague.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public int? PlayerId { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public string Bio { get; set; }
    }
}
