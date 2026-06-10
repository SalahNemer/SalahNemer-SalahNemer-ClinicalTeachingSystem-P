using DepartmentsTbl_CRUD.DTO;
using DevetionStudetns.DTO.DepartmentsDTO;
using loginpage.DBcon;

namespace DepartmentsTbl_CRUD.Service
{
    public interface IDepartmentsService
    {
        Task<IEnumerable<GetDepartmentsDto>> GetAllDepartments();
        Task<GetDepartmentsDto?> GetDepartmentsById(int id);
        Task<GeneralMsgDto> AddDepartment(AddDepartmentDto departmentsDTO);
        Task<GeneralMsgDto> UpdateDepartment(int id,  AddDepartmentDto departmentsDTO);
        Task<GeneralMsgDto> DeleteDepartment(int id);
    }
}
