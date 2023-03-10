using TaskManagement.Models;
using TaskManagement.Utils;

namespace TaskManagement.Interface
{
    public interface INotificationRepository
    {
        ResponseObject GetNotificationsByTask(int taskId);   
    }
}
