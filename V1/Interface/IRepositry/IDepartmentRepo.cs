using DevetionStudetns.NewFolder;
using loginpage.DBcon;

namespace DepartmentsTbl_CRUD.Repos
{
    public interface IDepartmentRepo
    {
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<Department?> GetDepartmentById(int id);
        Task<GeneralMsgDto> AddDepartmentAsync (Department department);
        Task<GeneralMsgDto> UpdateDepartmentAsync (Department department);
        Task<GeneralMsgDto> DeleteDepartmentAsync (int id);
    }
}
    