using TaskManagement.Models;
using TaskManagement.Utils;

namespace TaskManagement.Interface
{
    public interface IWorkSpaceRepository
    {
        ResponseObject WorkSpaces();
        WorkSpace GetWorkSpaceByID(int workSpaceID);
        ResponseObject GetWorkSpacesByUser(int userID);   
        ResponseObject GetWorkSpaceByCreateTime(int? month, int year);
        ResponseObject AddMemberIntoWorkspace(int workSpaceID, string userName, int roleID);
        ResponseObject CreateWorkSpace(int userID, int roleID, WorkSpace workSpaceCreate);
        ResponseObject UpdateWorkSpace(WorkSpace workSpace);
        ResponseObject DeleteWorkSpace(WorkSpace workSpace);
        bool Save();


    }
}
