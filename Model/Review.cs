using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AudioId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public int? Status { get; set; }

        public virtual Audio? Audio { get; set; }
        public virtual User? User { get; set; }
    }
}
