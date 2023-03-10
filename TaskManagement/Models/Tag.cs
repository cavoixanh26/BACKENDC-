using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
