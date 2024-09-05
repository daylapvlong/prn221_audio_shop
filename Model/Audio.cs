using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class Audio
    {
        public Audio()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int? ArtistId { get; set; }
        public int? GenreId { get; set; }
        public int? MoodId { get; set; }
        public bool? Status { get; set; }
        public string? Filename { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }

        public virtual User? Artist { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual Mood? Mood { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
