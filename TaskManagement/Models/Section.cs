using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class Section
    {
        public Section()
        {
            Tasks = new HashSet<Task>();
            UserSectionRoles = new HashSet<UserSectionRole>();
        }

        public int Id { get; set; }
        public int WorkSpaceId { get; set; }
        public string Title { get; set; } = null!;
        public string? Describe { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool Status { get; set; }

        public virtual WorkSpace WorkSpace { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<UserSectionRole> UserSectionRoles { get; set; }
    }
}
