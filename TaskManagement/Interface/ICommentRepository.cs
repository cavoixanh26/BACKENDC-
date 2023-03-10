using TaskManagement.Models;
using TaskManagement.Utils;

namespace TaskManagement.Interface
{
    public interface ICommentRepository
    {
        ResponseObject GetCommentsByTask(int taskID);
        Comment GetCommentById(int commentID);
        ResponseObject AddComment(int userID, Comment comment);
        ResponseObject DeleteComment(int userID, int commentID);
        ResponseObject UpdateComment(int userID, Comment comment);

    }
}
