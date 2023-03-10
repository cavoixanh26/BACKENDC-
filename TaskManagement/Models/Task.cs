using System;
using System.Collections.Generic;

namespace TaskManagement.Models
{
    public partial class Task
    {
        public Task()
        {
            Comments = new HashSet<Comment>();
            Notifications = new HashSet<Notification>();
            UserTaskRoles = new HashSet<UserTaskRole>();
        }

        public int Id { get; set; }
        public int? SectionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Describe { get; set; }
        public string? Image { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;   
        public DateTime? TaskTo { get; set; }
        public DateTime? TaskFrom { get; set; }
        public bool PinTask { get; set; }
        public int? TagId { get; set; }
        public string? Attachment { get; set; }

        public virtual Section? Section { get; set; }
        public virtual Tag? Tag { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserTaskRole> UserTaskRoles { get; set; }
    }
}
