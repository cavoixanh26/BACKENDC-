using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserActiveId { get; set; }
        public int? UserPassiveId { get; set; }
        public string Describe { get; set; } = null!;
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        public virtual Task Task { get; set; } = null!;
    }
}
