using DepartmentsTbl_CRUD.DTO;
using DepartmentsTbl_CRUD.Mapper;
using DepartmentsTbl_CRUD.Repos;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.DTO.DepartmentsDTO;
using loginpage.DBcon;
using loginpage.ErrorMsgs;

namespace DepartmentsTbl_CRUD.Service
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDepartmentRepo  _departmentRepo;
        public DepartmentsService( IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public async Task<IEnumerable<GetDepartmentsDto>> GetAllDepartments()
        {
            var departments = await _departmentRepo.GetAllDepartment();
            return departments.Select(de => DepartmentsMapper.ShowDepartmentMapper(de)).ToList();
        }
        public async Task<GetDepartmentsDto?> GetDepartmentsById(int id)
        {
            var department = await _departmentRepo.GetDepartmentById(id);
            return department != null ? DepartmentsMapper.ShowDepartmentMapper(department) : null; 
        }
       
        public async Task<GeneralMsgDto> AddDepartment(AddDepartmentDto departmentsDTO)
        {
            return await _departmentRepo.AddDepartmentAsync(DepartmentsMapper.AddOrEditDepartmentMapper(departmentsDTO));          
        }

        public async Task<GeneralMsgDto> UpdateDepartment(int id, AddDepartmentDto departmentsDTO)
        {
            var department = await _departmentRepo.GetDepartmentById(id);
            if (department == null)
            {
                return new GeneralMsgDto(IErrorMsgs.DEPARTMENT_NOT_FOUND,
                                  "خطأ بإدخال القسم  ",
                                    "القسم غير موجود. يرجى التأكد من القسم"
                                                );
            }

            department.DepartmentName = departmentsDTO.DepartmentName;
            return (await _departmentRepo.UpdateDepartmentAsync(department));
        }
        public async Task<GeneralMsgDto> DeleteDepartment(int id)
        {
            return await _departmentRepo.DeleteDepartmentAsync(id);
        }
    }
}
