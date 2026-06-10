using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DepartmentsTbl_CRUD.Repos
{
    public class DepartmentsRepo : IDepartmentRepo
    {
        private readonly DBC _appDbContext;
        public DepartmentsRepo(DBC appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            try
            {
                return (await _appDbContext.Departments.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<Department?> GetDepartmentById(int id)
        {
            try
            {
                return await _appDbContext.Departments.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<GeneralMsgDto> AddDepartmentAsync(Department department)
        {
            try
            {
                var existingDepartment = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentName == department.DepartmentName);
                if (existingDepartment != null)
                {
                    return new GeneralMsgDto(
                             IErrorMsgs.DEPARTMENT_ALREADY_EXISTS,
                       "خطأ في الإضافة",
                       "هذا القسم موجود بالفعل. يرجى اختيار اسم آخر."
                           );
                }

                await _appDbContext.Departments.AddAsync(department);
                await _appDbContext.SaveChangesAsync();

               return new GeneralMsgDto(SuccessfullyMsgs.DEPARTMENT_ADDED_SUCCESSFULLY,
                   "تمت الاضافة", 
                   "تم اضافة القسم بنجاح. شكراً لك"
                   );
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<GeneralMsgDto> UpdateDepartmentAsync(Department department)
        {
            try
            {
                var existingDepartment = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentName == department.DepartmentName);
                if (existingDepartment != null)
                {
                    return new GeneralMsgDto(
                             IErrorMsgs.DEPARTMENT_ALREADY_EXISTS,
                       "خطأ في الإضافة",
                       "هذا القسم موجود بالفعل. يرجى اختيار اسم آخر."
                           );
                }
                _appDbContext.Departments.Update(department);
                await _appDbContext.SaveChangesAsync();
                return new GeneralMsgDto(SuccessfullyMsgs.DEPARTMENT_UPDATED_SUCCESSFULLY,
                  " تم التحديث",
                  "تم تحديث اسم القسم بنجاح. شكراً لك"
                  );
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<GeneralMsgDto> DeleteDepartmentAsync(int id)
        {
            try
            {
                var checkDepartment = await _appDbContext.Departments
               .Include(d => d.doctors)
               .Include(d => d.Courses)
                  .FirstOrDefaultAsync(d => d.DepartmentId == id);


                var department = await _appDbContext.Departments.FindAsync(id);
                if (department == null)
                {
                    return new GeneralMsgDto(IErrorMsgs.DEPARTMENT_NOT_FOUND,
                                     "خطأ بادخال القسم  ",
                                     "القسم غير موجود. يرجى التأكد من القسم"
                                         );
                }

                List<string> usedInTables = new List<string>();

                if (department.doctors.Any())
                    usedInTables.Add("جدول الأطباء");

                if (department.Courses.Any())
                    usedInTables.Add("جدول الكورسات");

                if (usedInTables.Count > 0)
                {
                    string tablesList = string.Join(" و ", usedInTables);
                    return new GeneralMsgDto(
                        IErrorMsgs.DEPARTMENT_IN_USE,
                        "خطأ في الحذف",
                        $"لا يمكنك حذف هذا القسم لأنه مستخدم في {tablesList}."
                    );
                }

                    _appDbContext.Departments.Remove(department);
                    await _appDbContext.SaveChangesAsync();

                    return new GeneralMsgDto(SuccessfullyMsgs.DEPARTMENT_DELETED_SUCCESSFULLY,
                                    " تم الحذف",
                                    "تم حذف القسم بنجاح. شكراً لك"
                                         );
            }
            
            catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 547)
            {

                return new GeneralMsgDto(
                             IErrorMsgs.DEPARTMENT_DELETE_FAILED,
                             "خطأ في الحذف",
                                 "حدث خطأ أثناء حذف القسم. يرجى المحاولة لاحقًا."
                                   );
            }
        }
    }
}
