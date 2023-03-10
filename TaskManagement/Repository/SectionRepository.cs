using AutoMapper;
using TaskManagement.Dto;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Service;
using TaskManagement.Utils;
//using static System.Collections.Specialized.BitVector32;

namespace TaskManagement.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly TaskManagementContext _context;
        private readonly IMapper _mapper;
        public SectionRepository(TaskManagementContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Section GetSectionById(int SectionID)
        {
            return  _context.Sections.Where(s => s.Id == SectionID).FirstOrDefault();
          
        }

        public ResponseObject GetSections()
        {
            var sections = _context.Sections.ToList();
            var sectionMap = _mapper.Map<List<SectionDto>>(sections);
            if (sections != null)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = sectionMap
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
        public ResponseObject GetSectionsByName(int workspaceID,string sectionName)
        {
            
            var sections = _context.Sections.Where(s => s.Title == sectionName && s.WorkSpaceId == workspaceID).ToList();
            var sectionMap = _mapper.Map<List<SectionDto>>(sections);
            if (sections != null)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = sectionMap
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

        public ResponseObject GetSectionsInWorkspace(int WorkspaceID)
        {
            var sections = _context.Sections.Where(s => s.WorkSpaceId == WorkspaceID).ToList();
            var sectionsMap = _mapper.Map<List<SectionDto>>(sections);
            if (sections != null)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = sectionsMap
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

        public ResponseObject GetSectionsUserJoined(int workspaceID, int userID)
        {
            var sections = from s in _context.Sections
                           join usr in _context.UserSectionRoles on s.Id equals usr.SectionId
                           where usr.UserId == userID
                           select s;
            sections.Where(s => s.WorkSpaceId == workspaceID).ToList();

            var sectionsMap = _mapper.Map<List<SectionDto>>(sections);
            if (sections != null)
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = sectionsMap
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

       
        public ResponseObject AddMemberIntoSection(int sectionID, int userID, int roleID)
        {
            var user = _context.Users.Where(u => u.Id == userID).FirstOrDefault();
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleID);
            var section = _context.Sections.FirstOrDefault(s => s.Id== sectionID);
            var _user = _context.UserSectionRoles.Where(o => o.UserId== userID && o.SectionId == sectionID).FirstOrDefault();


            if (section == null|| user == null || role == null)
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound,
                    Data = null,
                };
            }
            if (_user!= null)
            {
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            }
            var roleSection = new UserSectionRole
            {
                UserId = user.Id,
                RoleId = roleID,
                SectionId = sectionID,
            };
            _context.UserSectionRoles.Add(roleSection);
            var userMap = _mapper.Map<UserDto>(user);
            if (Save())
            {
                return new ResponseObject
                {
                    Status = Status.Created,
                    Message = Message.Created,
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

       

        public ResponseObject CreateSection(int userID,int roleID, Section section)
        {
            var user = _context.Users.Where(u => u.Id == userID).FirstOrDefault();
            var role = _context.Roles.Where(r => r.Id == roleID).FirstOrDefault();
            
            if (user == null)
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound + " userID",
                    Data = null
                };
            }
            if (role == null)
            {
                return new ResponseObject
                {
                    Status = Status.NotFound,
                    Message = Message.NotFound + " roleID",
                    Data = null
                };
            }
            var userSectionRole = new UserSectionRole
            {
                UserId = userID,
                RoleId = roleID,
                SectionId = section.Id,
                Role = role,
                User = user,
                Section = section
            };
            _context.Add(userSectionRole);
            _context.Sections.Add(section);

            var sectionMap = _mapper.Map<SectionDto>(section);
            if (Save())
            {
                
                return new ResponseObject
                {
                    Status = Status.Created,
                    Message = Message.Created,
                    Data = sectionMap
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

        public ResponseObject DeleteSection(Section section)
        {
            _context.Sections.Remove(section);
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
                    Status = "Error",
                    Message = "Error",
                    Data = null
                };
            }
        }

     
       

        public ResponseObject UpdateSection(Section section)
        {
            _context.Sections.Update(section);
            var sectionMap = _mapper.Map<SectionDto>(section);
            if (Save())
            {
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = sectionMap
                };
            }
            else
            {
                return new ResponseObject
                {
                    Status = "Error",
                    Message = "Error",
                    Data = null
                };
            }
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SectionExists(int sectionID)
        => _context.Sections.Any(s => s.Id == sectionID);


        public Section DuplicateSection(int sectionID)
        {
            var oldSection = _context.Sections.FirstOrDefault(s => s.Id == sectionID); // kiểm tra xem section

            if (oldSection == null)
            {
                return null;
            }

            var newSection = new Section // add section mới vào db
            {
                Title = oldSection.Title + " (Copy)",
                Describe = oldSection.Describe,
                WorkSpaceId = oldSection.WorkSpaceId,
                CreatedTime = DateTime.Now,
                Status = oldSection.Status,
            };



            _context.Sections.Add(newSection);

            if (Save())
            {
                return newSection;
            }
            else
            {
                return null;
            }
        }

        public ResponseObject DuplicateTask(int newsectionID, int oldSectionID, int userID, int roleID) // 
        {
            //newsectionID: section đã được duplicated
            //oldSectionID: section duplicated

            var getTaskInSection = _context.Tasks.Where(o => o.SectionId == oldSectionID).ToList(); // lấy toàn bộ task trong section cũ 

            List<Models.Task> listTaskInSection = new List<Models.Task>();
            foreach (var task in getTaskInSection) // duplicate task 
            {
                listTaskInSection.Add(new Models.Task
                {
                    Title = task.Title + "(copy)",
                    Describe = task.Describe,
                    SectionId = newsectionID,
                    Image = task.Image,
                    CreatedTime = DateTime.Now,
                    TaskTo = task.TaskTo,
                    TaskFrom = task.TaskFrom,
                    PinTask = task.PinTask,
                    TagId = task.TagId,
                    Attachment = task.Attachment,
                    Status = task.Status,
                });

            }
            _context.Tasks.AddRange(listTaskInSection);
            _context.SaveChanges();

            List<UserTaskRole> roles = new List<UserTaskRole>();
            foreach (var item in listTaskInSection) // add role cho các task (ở đây chi duplicate người tạo)
            {
                roles.Add(new UserTaskRole
                {
                    TaskId = item.Id,
                    RoleId = roleID,
                    UserId = userID,
                });
            }
            var userSection = new UserSectionRole // add role cho các section (ở đây chi duplicate người tạo)
            {
                SectionId = newsectionID,
                UserId = userID,
                RoleId = roleID,
            };
            _context.UserTaskRoles.AddRange(roles);
            _context.UserSectionRoles.Add(userSection);
            _context.SaveChanges();

            return new ResponseObject
            {
                Status = Status.Success,
                Message = Message.Success,
                Data = null
            };

        }
    }
}
