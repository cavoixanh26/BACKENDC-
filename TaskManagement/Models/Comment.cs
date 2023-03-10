using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Task Task { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
