using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TaskManagement.Dto;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Repository;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("User/{username}")]
        public IActionResult GetUserByUserName(string username)
        {
            var user = _userRepository.GetUserByUserName(username);
            return Ok(user);
        }

        [HttpGet("Workspace/{workspaceID}")]
        public IActionResult GetUsersJoinWorkSpace(int workspaceID)
        {
            var users = _userRepository.GetUsersJoinWorkSpace(workspaceID);
            return Ok(users);
        }

        [HttpGet("Section/{sectionID}")]
        public IActionResult GetUsersJoinSection(int sectionID)
        {
            var users = _userRepository.GetUsersJoinSection(sectionID);
            return Ok(users);
        }

        [HttpGet("Task/{taskID}")]
        public IActionResult GetUsersJoinTask(int taskID)
        {
            var users = _userRepository.GetUsersJoinTask(taskID);
            return Ok(users);
        }

        [HttpGet("UnJoinTaskInWorkSpace/{taskID}")]
        public IActionResult GetUsersUnJoinTask(int taskID)
        {
            var users = _userRepository.GetUsersUnJoinTask(taskID);
            return Ok(users);   
        }
        // POST api/<UserController>
        //[HttpPost]
        //public IActionResult CreateUser([FromBody] UserDto userCreate)
        //{
        //    var userMap = _mapper.Map<User>(userCreate);
        //    var create = _userRepository.CreateUser(userMap);
        //    return Ok(create);
        //}

        // PUT api/<UserController>/5
        [HttpPut("{userId}")]
        public IActionResult UpdateUser([FromForm]int userId, [FromBody]UserDto updatedUser)
        {
            var userMap = _mapper.Map<User>(updatedUser);
            var update = _userRepository.UpdateUser(userMap);
            return Ok(update);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var userToDelete = _userRepository.GetUserByID(userId);
            var delete = _userRepository.DeleteUser(userToDelete);
            return Ok(delete);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User us)
        {
            var result = _userRepository.Login(us);
            return Ok(result);
        }
        [HttpPut("Image/{userID}")]
        public async Task<IActionResult> UpdateImage(IFormFile file, int userID)
        {
            var img = await _userRepository.UploadAsync(file);
            var update = _userRepository.UpdateImage(userID, img.Uri.ToString());
            return Ok(update);
        }

    }
}
