using DataBase.DBcon;
using DepartmentsTbl_CRUD.DTO;
using DepartmentsTbl_CRUD.Service;
using DevetionStudetns.Error.SuccessfullyMsg;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepartmentsTbl_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;
        private readonly DBC _dbContext; 
        public DepartmentsController(IDepartmentsService departmentsService, DBC dbContext)
        {
            _departmentsService = departmentsService;
            _dbContext = dbContext;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<ActionResult<IEnumerable<AddDepartmentDto>>> GetAllDepartments ()
        {
            try
            {
                var GetDepartments = _dbContext.Departments.ToList().Count; 
                if(GetDepartments == 0 )
                {
                    GeneralMsgDto errorMsg = new GeneralMsgDto(
                        IErrorMsgs.EMPTY_DEPARTMENTS_TABLE, "خطأ في البحث","لا يوجد أقسام لعرضها. يرجى إضافة أقسام أولاً"
                        );
                    return BadRequest(errorMsg);
                }
                return Ok(await _departmentsService.GetAllDepartments());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetDepartmentById{id}")]
        public async Task<ActionResult<AddDepartmentDto>> GetDepartmentById (int id )
        {
            try
            {
                var getDepartment   = _dbContext.Departments.Where(d =>d.DepartmentId == id).ToList().Count;
                if(getDepartment == 0)
                {
                    GeneralMsgDto erorMsg = new GeneralMsgDto(
                        IErrorMsgs.DEPARTMENT_NOT_FOUND, "خطأ في البحث", "القسم غير موجود. يرجى التحقق من المعرف المدخل."
                        );
                    return BadRequest(erorMsg);
                }    
                return Ok(await _departmentsService.GetDepartmentsById(id)); 
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment (AddDepartmentDto departmentDTO)
        {
            try
            {
                var resutl = await _departmentsService.AddDepartment(departmentDTO);
                if ( resutl.ErrorMsg == "تم اضافة القسم بنجاح")
                    return Ok(resutl);  
                return BadRequest(resutl);  
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpPut("UpdateDepartment{id}")]
        public async Task<IActionResult> UpdateDepartment(int id , AddDepartmentDto departmentsDTO)
        {
            try
            {
                var result =  await _departmentsService.UpdateDepartment(id, departmentsDTO);  
                if ( result.ErrorMsg == "تم تحديث القسم بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("DeleteDepartment{id}")]
        public async Task<IActionResult> DeleteDepartment (int id)
        {
            try
            {
                return Ok(await _departmentsService.DeleteDepartment(id));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
    }
}
