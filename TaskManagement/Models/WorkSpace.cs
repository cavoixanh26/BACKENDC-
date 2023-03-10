using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class WorkSpace
    {
        public WorkSpace()
        {
            Sections = new HashSet<Section>();
            UserWorkSpaceRoles = new HashSet<UserWorkSpaceRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Describe { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<UserWorkSpaceRole> UserWorkSpaceRoles { get; set; }
    }
}
