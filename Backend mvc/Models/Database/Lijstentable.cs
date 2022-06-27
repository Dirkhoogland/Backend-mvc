using System;
using System.Collections.Generic;

namespace Backend_mvc.Models.Database
{
    public partial class lijstentable
    {
        public lijstentable()
        {
            Tasks = new HashSet<Tasks>();
        }

        public int IdLijst { get; set; }
        public string NaamLijst { get; set; } = null!;

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
