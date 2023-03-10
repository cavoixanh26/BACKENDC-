using TaskManagement.Models;
using TaskManagement.Utils;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Interface
{
    public interface ISectionRepository
    {
        ResponseObject GetSections();
        Section GetSectionById(int SectionID);
        ResponseObject GetSectionsByName(int workspaceID,string sectionName);
        ResponseObject GetSectionsInWorkspace(int WorkspaceID);

        //Hàm này sẽ trả về danh sách các section mà một user cụ thể tham gia.
        ResponseObject GetSectionsUserJoined(int workspaceID, int userID);
        ResponseObject AddMemberIntoSection(int sectionID, int userId, int roleID);
        ResponseObject CreateSection(int userID, int roleID, Section section);
        ResponseObject UpdateSection(Section section);
        ResponseObject DeleteSection(Section section);
        bool SectionExists(int sectionID);
        bool Save();

        Section DuplicateSection(int sectionID);
        ResponseObject DuplicateTask(int newsectionID, int oldSectionID, int userID, int roleID);

    }
}
