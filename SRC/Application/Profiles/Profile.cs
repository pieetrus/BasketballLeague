using BasketballLeague.Domain.Entities;
using System.Collections.Generic;

namespace BasketballLeague.Application.Profiles
{
    public class Profile
    {
        public Profile()
        {
            Photos = new HashSet<Photo>();
        }

        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public ICollection<Photo> Photos { get; set; }

    }
}
