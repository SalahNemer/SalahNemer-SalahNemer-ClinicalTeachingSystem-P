using DepartmentsTbl_CRUD.DTO;
using DevetionStudetns.DTO.DepartmentsDTO;
using DevetionStudetns.NewFolder;

namespace DepartmentsTbl_CRUD.Mapper
{
    public static class DepartmentsMapper
    {
        public static Department AddOrEditDepartmentMapper(this AddDepartmentDto department)
        {
            return new Department
            {
                DepartmentName = department.DepartmentName,

            };
        }
        public static GetDepartmentsDto ShowDepartmentMapper (this Department department)
        {
            return new GetDepartmentsDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
            }; 
        }  
        
    }
}
