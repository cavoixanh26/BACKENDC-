using CloudinaryDotNet.Actions;
using System.Threading.Tasks;
using TaskManagement.Models;
using TaskManagement.Utils;
using static System.Collections.Specialized.BitVector32;

namespace TaskManagement.Interface
{
    public interface IUserRepository
    {
       
        ResponseObject GetUsers();
        User GetUserByID(int userId);

        ResponseObject GetUserByUserName(string userName);
        ResponseObject GetUsersJoinWorkSpace(int workSpaceID);
        //Hàm này sẽ trả về danh sách các user tham gia vào một section cụ thể.
        ResponseObject GetUsersJoinSection(int sectionID);

        //Hàm này sẽ trả về danh sách các user đang tham gia vào một nhiệm vụ cụ thể.
        ResponseObject GetUsersJoinTask(int taskID);
        
        ResponseObject GetUsersUnJoinTask(int taskID);// những user đã join vào ws nhưng chưa join vào task
        ResponseObject CreateUser(User user);
        ResponseObject UpdateUser(User user);
        ResponseObject DeleteUser(User user);

        ResponseObject Login(User user);
        bool UserExists(int userId);
        bool UserNameExists(string userName);
        bool Save();
        Task<UploadResult> UploadAsync(IFormFile file);
        ResponseObject UpdateImage(int userID, string file);

    }
}
