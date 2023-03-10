using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP_Login.Utils;
using System.Data;
using TaskManagement.Dto;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Repository;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceController : ControllerBase
    {
        private readonly IWorkSpaceRepository _workSpaceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public WorkSpaceController(IWorkSpaceRepository workSpaceRepository,
            IUserRepository userRepository , IMapper mapper)
        {
            _workSpaceRepository = workSpaceRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //[HttpGet, Authorize(Roles  = "Basic")]
        [HttpGet]
        public IActionResult WorkSpaces()
        {
            var workSpaces = _workSpaceRepository.WorkSpaces();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(workSpaces);
        }

        [HttpGet("Id/{id}")] // sẽ bỏ trong tương lai, để lại để test
        public IActionResult GetWorkSpaceByID(int id)
        {
            var ws = _workSpaceRepository.GetWorkSpaceByID(id);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(ws);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetWorkSpaceByUser(int userId) 
        {
            var ws = _workSpaceRepository.GetWorkSpacesByUser(userId);
            return Ok(ws);
        }

        //[JwtFilter]
        [HttpPost]
        public IActionResult CreateWorkSpace([FromQuery]int userID, [FromQuery] int roleID,
            [FromBody] WorkSpaceDto workSpaceCreate)
        {
            var workspaceMap = _mapper.Map<WorkSpace>(workSpaceCreate);
            var created = _workSpaceRepository.CreateWorkSpace(userID, roleID, workspaceMap);
            return Ok(created);
        }


        [HttpPut("{workspaceID}")]
        public IActionResult UpdateWorkSpace(int workspaceID,[FromBody] WorkSpaceDto workSpaceUpdate)
        {
            var workspaceMap = _mapper.Map<WorkSpace>(workSpaceUpdate);
            var update = _workSpaceRepository.UpdateWorkSpace(workspaceMap);
            return Ok(update);
        }

        [HttpDelete("{workSpaceID}")]
        public IActionResult DeleteWorkSpace(int workSpaceID)
        {
            var workspace = _workSpaceRepository.GetWorkSpaceByID(workSpaceID);
            return Ok(_workSpaceRepository.DeleteWorkSpace(workspace));
        }




        [HttpPost("AddMember/{workSpaceID}")]
        // add thành viên vào trong ws
        public IActionResult AddMemberIntoWorkspace(int workSpaceID, string nameUser, int roleID)
        {
            var addMember = _workSpaceRepository.AddMemberIntoWorkspace(workSpaceID, nameUser, roleID);
            return Ok(addMember);
        }

        [HttpGet("createTime")]
        // tìm ws theo thời gian

        public IActionResult GetWorkSpaceByCreateTime(int? month, int year)
        {
            var ws = _workSpaceRepository.GetWorkSpaceByCreateTime(month, year);

            return Ok(ws);
        }



    }
}
