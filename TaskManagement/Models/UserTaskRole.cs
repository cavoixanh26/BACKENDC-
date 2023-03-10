using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class UserTaskRole
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
