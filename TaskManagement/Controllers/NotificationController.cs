using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Interface;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet("task/{taskID}")]
        public IActionResult GetNotificationsByTask(int taskID)
        {
            var noti = _notificationRepository.GetNotificationsByTask(taskID);
            return Ok(noti);
        }
    }
}
