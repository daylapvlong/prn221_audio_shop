using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class Mood
    {
        public Mood()
        {
            Audios = new HashSet<Audio>();
        }

        public int Id { get; set; }
        public string? MoodName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Audio> Audios { get; set; }
    }
}
