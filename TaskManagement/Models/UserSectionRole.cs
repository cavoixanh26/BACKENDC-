using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class UserSectionRole
    {
        public int UserId { get; set; }
        public int SectionId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Section Section { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
