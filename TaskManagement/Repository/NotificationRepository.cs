using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Service;
using TaskManagement.Utils;

namespace TaskManagement.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskManagementContext _context;

        public NotificationRepository(TaskManagementContext context)
        {
            _context = context;
        }
        public ResponseObject GetNotificationsByTask(int taskId)
        {
            var noti = _context.Tasks.Where(o => o.Id== taskId).Select(o => o.Notifications).ToList();   

            if (noti != null) { 
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = noti
                };
            } else
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null
                };
            }
        }
    }
}
