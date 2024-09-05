using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class Genre
    {
        public Genre()
        {
            Audios = new HashSet<Audio>();
        }

        public int Id { get; set; }
        public string? Genre1 { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Audio> Audios { get; set; }
    }
}
