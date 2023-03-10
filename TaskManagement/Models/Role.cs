using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class Role
    {
        public Role()
        {
            UserSectionRoles = new HashSet<UserSectionRole>();
            UserTaskRoles = new HashSet<UserTaskRole>();
            UserWorkSpaceRoles = new HashSet<UserWorkSpaceRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Describe { get; set; } = null!;

        public virtual ICollection<UserSectionRole> UserSectionRoles { get; set; }
        public virtual ICollection<UserTaskRole> UserTaskRoles { get; set; }
        public virtual ICollection<UserWorkSpaceRole> UserWorkSpaceRoles { get; set; }
    }
}
