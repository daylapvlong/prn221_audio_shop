using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class User
    {
        public User()
        {
            Audios = new HashSet<Audio>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            AudiosNavigation = new HashSet<Audio>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Audio> Audios { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<Audio> AudiosNavigation { get; set; }
    }
}
