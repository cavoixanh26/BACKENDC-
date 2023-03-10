using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class UserWorkSpaceRole
    {
        public int UserId { get; set; }
        public int WorkSpaceId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual WorkSpace WorkSpace { get; set; } = null!;
    }
}
