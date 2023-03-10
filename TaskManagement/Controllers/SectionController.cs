using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Dto;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Repository;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IWorkSpaceRepository _workSpaceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper
            , IWorkSpaceRepository workSpaceRepository
            , IUserRepository userRepository)
        {
            _sectionRepository = sectionRepository;
            _workSpaceRepository = workSpaceRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSections()
        {
                var sections = _sectionRepository.GetSections();
                return Ok(sections);
        }

        [HttpGet("Id/{sectionId}")]
        public IActionResult GetSectionById(int sectionId)
        {
            var section = _sectionRepository.GetSectionById(sectionId);
            return Ok(section);
        }

        [HttpGet("Name/{sectionName}")]
        public IActionResult GetSectionByName(int workspaceID, string sectionName)
        {
            var section = _sectionRepository.GetSectionsByName(workspaceID,sectionName);
            return Ok(section);
        }

        [HttpGet("Workspace/{workspaceId}")]
        public IActionResult GetSectionsInWorkSpace(int workspaceId)
        {
            var section = _sectionRepository.GetSectionsInWorkspace(workspaceId);
            return Ok(section);
        }

        [HttpGet("User/{userId}")]
        public IActionResult GetSectionsUserJoined(int workspaceID, int userId)
        {
            var section = _sectionRepository.GetSectionsUserJoined(workspaceID,userId);
            return Ok(section);
        }

        [HttpPost]
        public IActionResult CreateSection([FromQuery]int userID,[FromQuery]int roleID,[FromBody] SectionDto sectionCreate)
        {
                var sectionMap = _mapper.Map<Section>(sectionCreate);
                var create = _sectionRepository.CreateSection(userID, roleID, sectionMap);
                return Ok(create);
        }


        [HttpPost("{sectionID}/{userID}/{roleID}")]
        public IActionResult AddMemberIntoSection(int sectionID, int userID,int roleID)
        {
            var user = _sectionRepository.AddMemberIntoSection(sectionID, userID, roleID);
            return Ok(user);
        }

        [HttpPut("{sectionId}")]
       
        public IActionResult UpdateSection(int sectionId, [FromBody]SectionDto sectionUpdate)
        {
           
                var section = _mapper.Map<Section>(sectionUpdate);
                var update = _sectionRepository.UpdateSection(section);
                return Ok(update);
            
        }

        [HttpDelete("{sectionID}")]
        public IActionResult DeleteSection(int sectionID)
        {
            try
            {
                var section = _sectionRepository.GetSectionById(sectionID);
                var detele =  _sectionRepository.DeleteSection(section);
                return Ok(detele);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpPost("Duplicate")]
        public IActionResult Duplicate(int sectionID, int userID, int roleID)
        {
            var section = _sectionRepository.DuplicateSection(sectionID);
            var task = _sectionRepository.DuplicateTask(section.Id, sectionID, userID, roleID);
            return Ok(task);
        }


    }
}
