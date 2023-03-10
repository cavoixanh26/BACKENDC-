using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Collections.ObjectModel;
using TaskManagement.Dto;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Service;
using TaskManagement.Utils;
using static System.Collections.Specialized.BitVector32;

namespace TaskManagement.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementContext _context;
        private readonly IMapper _mapper;


        
        public TaskRepository(TaskManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public Models.Task GetTaskByID(int taskId)
        => _context.Tasks.SingleOrDefault(o => o.Id == taskId);

        public ResponseObject GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            var taskMap = _mapper.Map<List<TaskDto>>(tasks);

            if (tasks == null)
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                    Data = null,
                };
            else
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = taskMap,
                };
        }

        public ResponseObject GetAssignedTasksForUser(int workspaceID, int userID)
        {

            ICollection<Models.Task> tasks = new Collection<Models.Task>();
            
            if (workspaceID != 0)
            {
                 tasks = _context.UserTaskRoles.Where(utr => utr.UserId == userID && utr.RoleId == 2)
                    .Select(t => t.Task).Where(o => o.Section.WorkSpace.Id == workspaceID).ToList();
            } else
            {
                 tasks = _context.UserTaskRoles.Where(utr => utr.UserId == userID && (utr.RoleId == 2 || utr.RoleId == 1))
                    .Select(t => t.Task).Distinct().ToList();
            }
            
            var tasksMap = _mapper.Map<List<TaskDto>>(tasks);
            if (tasks != null)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = tasksMap
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null
                };
            }
        }

        public ResponseObject GetUnassignedTasks(int workspaceID)
        {
            var tasksAll = _context.Tasks.Where(t => t.Section.WorkSpaceId == workspaceID).ToList();
            var tasksAssign = _context.UserTaskRoles.Where(utr => utr.RoleId == 2)
            .Select(t => t.Task).Where(t => t.Section.WorkSpaceId == workspaceID).ToList();

            var tasks = tasksAll.Except(tasksAssign);
            var tasksMap = _mapper.Map<List<TaskDto>>(tasks);
            if (tasks != null)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = tasksMap
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null
                };
            }
        }

        public ResponseObject GetTaskCountOfUser(int workspaceID, int userId)
        {
            var tasksCount = _context.UserTaskRoles.Where(utr => utr.UserId == userId && utr.RoleId == 2)
            .Select(t => t.Task).Where(t => t.Section.WorkSpaceId == workspaceID).Count();

            if (tasksCount != 0)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = tasksCount,
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

        public ResponseObject GetTaskCountUserCompleted(int workspaceID, int userId, bool status)
        {
            var numberOfTaskDone = _context.UserTaskRoles.Where(utr => utr.UserId == userId && utr.RoleId == 2)
            .Select(t => t.Task).Where(t => t.Section.WorkSpaceId == workspaceID && t.Status == status).ToList();
            
            if (numberOfTaskDone == null)
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null,
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = numberOfTaskDone,
                };
            }
        }


        public ResponseObject CreateTask(int? sectionID, int userId, int roleId, Models.Task task)
        {
            var user = _context.Users.SingleOrDefault(o => o.Id == userId);
            var role = _context.Roles.SingleOrDefault(o => o.Id == roleId);
            var section = _context.Sections.SingleOrDefault(o => o.Id == sectionID);
            if (user == null|| role == null)
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null
                };

            var userTaskRole = new UserTaskRole
            {
                RoleId = roleId,
                UserId = userId,
                TaskId = task.Id,
                User = user,
                Role = role,
                Task = task
            };
            string noti = user.Name + " đã thêm " + task.Title;
            if (section != null)
                noti += " vào danh sách "+section.Title;

            var notifi = new Notification
            {
                TaskId = task.Id,
                UserActiveId = userId,
                UserPassiveId = null,
                Describe = noti,
                Task = task,
            };
            _context.Add(notifi);
            _context.Add(userTaskRole);
            _context.Tasks.Add(task);

            
            var taskMap = _mapper.Map<TaskDto>(task);
            if (Save())
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = taskMap
                };
            else
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest
                };
            }

        }


        public ResponseObject UpdateTask(Models.Task task)
        {

            _context.Tasks.Update(task);
            var taskMap = _mapper.Map<TaskDto>(task);

            if (Save())
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = taskMap
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                    Data = null
                };
            }

        }
        public ResponseObject DeleteTask(Models.Task task)
        {

            _context.Remove(task);
            if (Save())
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = null

                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest
                };
            }
        }



        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }



        public ICollection<Models.Task> TasksFilter(int workSpaceID, int? day, bool? done, int? numMenber)
        {

            var tasks = _context.Tasks.Where(t => t.Section.WorkSpaceId == workSpaceID)
                .ToList();


            if (day.HasValue)
            {
                tasks = tasks.Where(t => t.TaskTo != null && CalculateDay(t.TaskTo) > 0 && CalculateDay(t.TaskTo) <= day.Value).ToList();
            }
            if (done.HasValue)
            {
                tasks = tasks.Where(t => t.Status == done.Value).ToList();
            }
            if (numMenber.HasValue)
            {
                tasks = tasks.Where(t => CountMemberInTask(t.Id) < numMenber.Value).ToList();
            }

            return tasks;
        }

        private int CalculateDay(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                TimeSpan timeSince = (TimeSpan)(dateTime - DateTime.Now);
                return timeSince.Days;
            }
            return 0;
        }

        private int CountMemberInTask(int taskID)
        {
            return _context.UserTaskRoles.Where(t => t.TaskId == taskID && t.RoleId != 1).Count();
        }

        public ResponseObject AddMemberIntoTask(int taskID, int userID, int roleID)
        {
            var task = _context.Tasks.Where(o => o.Id == taskID).FirstOrDefault();
            var user = _context.Users.Where(o => o.Id == userID).FirstOrDefault();
            var role = _context.Roles.Where(o => o.Id == roleID).FirstOrDefault();
            var _user = _context.UserTaskRoles.Where(o => o.UserId == userID && o.TaskId == taskID).FirstOrDefault();
            var _userWS = _context.UserWorkSpaceRoles.Where(o => o.UserId == userID).FirstOrDefault();
            if (_userWS == null)
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            }
            if (_user != null)
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            }
            if (user == null || task == null || role == null)
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            }

            var addUserTask = new UserTaskRole
            {
                TaskId = taskID,
                RoleId = roleID,
                UserId = userID,
            };
            _context.UserTaskRoles.Add(addUserTask);
            var userMap = _mapper.Map<UserDto>(user);
            if (Save())
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = userMap
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                    Data = null
                };
            }
        }

        public ResponseObject GetTasksInSection(int sectionId)
        {
            var task = _context.Tasks.Where(o => o.SectionId == sectionId).ToList();
            var taskMap = _mapper.Map<List<TaskDto>>(task);
            if (task == null)
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                    Data = null
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = taskMap
                };
            }
        }

        public ResponseObject UpdateImage(int taskID, string file)
        {
            var task = _context.Tasks.Where(o => o.Id == taskID).FirstOrDefault();
            if (task != null)
            {
                task.Image = file;
                _context.Update(task);
                var taskMap = _mapper.Map<TaskDto>(task);
                if (Save())
                {
                    return new ResponseObject
                    {
                        Status = Status.Success,
                        Message = Message.Success,
                        Data = taskMap
                    };
                }
                else
                {
                    return new ResponseObject
                    {
                        Status = Status.BadRequest,
                        Message = Message.BadRequest,
                        Data = null
                    };
                }
            }
            else
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null
                };
            }
        }

        public ResponseObject GetTasksRangeByTime(int userID, DateTime? timeFrom, DateTime? timeTo)
        {
            //var task = _context.UserTaskRoles.Where(o => o.UserId == userID)
            //    .Select(o => o.Task).Where(o => (o.TaskFrom >= timeFrom o.TaskFrom ) )

            //if (task != null)
            //{
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    //Data = task
                };
            //}
            //else
            //{
            //    return new ResponseObject
            //    {
            //        Status = Status.NotFound,
            //        Message = Message.NotFound,
            //    };
            //}
        }
    }
}
