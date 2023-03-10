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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet("task/{taskID}")]
        public IActionResult GetCommentsByTask(int taskID)
        {
            var com = _commentRepository.GetCommentsByTask(taskID); 
            return Ok(com);
        }

        [HttpPost]
        public IActionResult CreateComment(int userID, [FromBody] CommentDto commentCreate)
        {
            var commentMap = _mapper.Map<Comment>(commentCreate);
            var comment = _commentRepository.AddComment(userID, commentMap);
            return Ok(comment);
        }
        [HttpDelete("{commentID}")]
        public IActionResult DeleteComment(int userID, int commentID)
        {
            var comment = _commentRepository.DeleteComment(userID, commentID);
            return Ok(comment);
        }
        [HttpPut("{commentID}")]
        public IActionResult UpdateComment(int userID
            , [FromBody] CommentDto commentUpdate)
        {
            var comment = _mapper.Map<Comment>(commentUpdate);  
            var update = _commentRepository.UpdateComment(userID, comment); 
            return Ok(update);
        }
    }
}
