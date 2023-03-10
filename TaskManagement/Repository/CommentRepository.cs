using AutoMapper;
using TaskManagement.Dto;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.Service;
using TaskManagement.Utils;

namespace TaskManagement.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskManagementContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(TaskManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ResponseObject AddComment(int userID, Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    return new ResponseObject
                    {
                        Status = Status.BadRequest,
                        Message = Message.BadRequest,
                    };
                }
                if (userID != comment.UserId)
                {
                    return new ResponseObject
                    {
                        Status = Status.BadRequest,
                        Message = Message.BadRequest,
                    };
                }
                _context.Comments.Add(comment);
                _context.SaveChanges();
                var commentMap = _mapper.Map<CommentDto>(comment); 
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data = commentMap
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseObject DeleteComment(int userID, int commentID)
        {
            var comment = GetCommentById(commentID);
            if (comment == null)
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            if (comment.UserId != userID)
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            try
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Comment GetCommentById(int commentID)
         => _context.Comments.FirstOrDefault(o => o.Id == commentID);

        public ResponseObject GetCommentsByTask(int taskID)
        {
           
            var comments = _context.Comments.Where(o => o.TaskId == taskID).ToList();   
            if (comments.Count == 0)
                return new ResponseObject { Status = Status.NotFound,
                Message = Message.NotFound };
            var comsMap = _mapper.Map<List<CommentDto>>(comments);
            return new ResponseObject
            {
                Status = Status.Success,
                Message = Message.Success,
                Data = comsMap
            };
        }

        public ResponseObject UpdateComment(int userID, Comment comment)
        {
            if (comment == null)
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            if (comment.UserId != userID)
                return new ResponseObject
                {
                    Status = Status.BadRequest,
                    Message = Message.BadRequest,
                };
            try
            {
                _context.Comments.Update(comment);
                _context.SaveChanges();
                var commentMap = _mapper.Map<CommentDto>(comment); 
                return new ResponseObject
                {
                    Status = Status.Success,
                    Message = Message.Success,
                    Data= commentMap    
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
