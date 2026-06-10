using DataBase.Entity;
using System;
using DevetionStudetns.Interface;
using DataBase.DBcon;
using Microsoft.EntityFrameworkCore;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using DevetionStudetns.Error.SuccessfullyMsg;
using V1.DTO.TADTO;
using DevetionStudetns.DTO.AttendanceDTO;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.Repositry.TARepositry
{
    public class TARepo : ITARepo
    {
        private readonly DBC _appDbContext;
        public TARepo(DBC appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public async Task<IEnumerable<GetTAInformationQDto>> GetAllTAs()
        {
            var query = @"
                         SELECT 
                                t.TAId,
                                u.FullName as TaName, 
                                u.Email,
                                t.SuperVisedYear
                         FROM TA t
                         INNER JOIN Users u ON t.TAId = u.UserId";
            var result = await _appDbContext.Database
                                            .SqlQueryRaw<GetTAInformationQDto>(query)
                                            .ToListAsync();
            return result;
        }
        public async Task<TA?> GetTAById(string id)
        {
            return await _appDbContext.TA.FindAsync(id);
        }
        public async Task<GeneralMsgDto> AddTA(TA t)
        {
            if (!await _appDbContext.Users.AnyAsync(u => u.UserId == t.TAId && u.RoleId == 6))
            {
                var errorMsg = new GeneralMsgDto(
                            IErrorMsgs.USER_NOT_FOUND_OR_ROLE_INVALID,
                            "المعرف غير موجود أو غير مصرح له",
                            "لم يتم العثور على المستخدم أو أنه لا يمتلك الصلاحيات المطلوبة. يرجى التأكد من صحة البيانات المدخلة."
                                    );
                return errorMsg;
            }

            if (await _appDbContext.TA.AnyAsync(ta => ta.TAId == t.TAId))
            {
                var errorMsg = new GeneralMsgDto(
                         IErrorMsgs.SEARCH_AND_TEACHING_ASSISTANT_ALREADY_EXISTS,
                            "مساعد البحث والتدريس موجود بالفعل",
                            "مساعد البحث والتدريس الذي يحمل هذا المعرف موجود بالفعل في النظام. يرجى التحقق من صحة المعرف المدخل."
                                    );
                return errorMsg;

            }
            if (t.SupervisedYear < 4 || t.SupervisedYear > 6)
            {
                var errorMsg = new GeneralMsgDto(
                            IErrorMsgs.SUPERVISED_YEAR_INVALID,
                             "السنة المشرف عليها غير صحيحة",
                                 "السنة المشرف عليها يجب أن تكون 4 أو 5 أو 6. الرجاء التحقق من السنة المدخلة."
                                        );
                return errorMsg;
            }

             await _appDbContext.TA.AddAsync(t);
             await _appDbContext.SaveChangesAsync() ;

            var successMsg = new GeneralMsgDto(
                            SuccessfullyMsgs.ADDITION_SUCCESSFUL,
                        "تمت الإضافة بنجاح",
                        "تم إضافة البيانات بنجاح. يمكنك الآن متابعة الإجراءات التالية."
                     );
            return successMsg;

        }
        public async Task<GeneralMsgDto> UpdateTA(TA t)
        {
            var existingTA = await _appDbContext.TA.FindAsync(t.TAId);
            if (existingTA == null)
            {
                var errorMsg = new GeneralMsgDto(
                                 IErrorMsgs.SEARCH_AND_TEACHING_ASSISTANT_NOT_FOUND,
                              "مساعد البحث والتدريس غير موجود",
                            "لم يتم العثور على مساعد البحث والتدريس بالمعرف المدخل. يرجى التأكد من صحة البيانات المدخلة والمحاولة مجددًا."
                         );
                return errorMsg;
            }

            if (!await _appDbContext.Users.AnyAsync(u => u.UserId == t.TAId && u.RoleId == 6))
            {
                var errorMsg = new GeneralMsgDto(
                    IErrorMsgs.USER_NOT_FOUND_OR_ROLE_INVALID,
                    "المستخدم غير موجود أو غير مصرح له",
                    "لم يتم العثور على المستخدم أو أنه لا يمتلك الصلاحيات المطلوبة. يرجى التأكد من صحة البيانات المدخلة."
                );
                return errorMsg;
            }

            if (t.SupervisedYear < 4 || t.SupervisedYear > 6)
            {
                var errorMsg = new GeneralMsgDto(
                            IErrorMsgs.SUPERVISED_YEAR_INVALID,
                             "السنة المشرف عليها غير صحيحة",
                                 "السنة المشرف عليها يجب أن تكون 4 أو 5 أو 6. الرجاء التحقق من السنة المدخلة."
                                        );
                return errorMsg;

            }         
            
            existingTA.SupervisedYear = t.SupervisedYear; 
             await _appDbContext.SaveChangesAsync();
            var successMsg = new GeneralMsgDto(
                            SuccessfullyMsgs.UPDATE_SUCCESSFUL,
                                 "تم التحديث بنجاح",
                                     "تم تحديث البيانات بنجاح. شكراً لك."
                                                );
            return successMsg;
        }
        public async Task<GeneralMsgDto> DeleteTA(string id)
        {
            var ta = await _appDbContext.TA.FindAsync(id);
            if (ta == null)
            {
                var errorMsg = new GeneralMsgDto(
                            IErrorMsgs.SEARCH_AND_TEACHING_ASSISTANT_NOT_FOUND,
                                     "مساعد البحث والتدريس غير موجود",
                                         "لم يتم العثور على مساعد البحث والتدريس بهذا المعرف. الرجاء التحقق من البيانات المدخلة."
                                                    );
                return errorMsg;
            }

            _appDbContext.TA.Remove(ta);
             await _appDbContext.SaveChangesAsync();
            var successMsg = new GeneralMsgDto(
                              SuccessfullyMsgs.DELETE_SUCCESSFUL,
                                     "تم الحذف بنجاح",
                                     "تم حذف البيانات بنجاح.شكراً لك"
                                            );
            return successMsg;
        }
        public async Task<GeneralMsgDto> UpdateTaAndUser(UpdateTaAndUserDto ta, string taid)
        {
            var TaEmp = await _appDbContext.TA.FirstOrDefaultAsync(t => t.TAId == taid);
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserId == ta.UserId);

            if (TaEmp == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                   IErrorMsgs.Research_And_Teaching_Assistant_ID_Not_Found,
                   "Fialed",
                   "There is not any Studnets have smae this id  : " + taid
                   );
                return ErrorMsg;
            }
            else
            {
                bool isTaDataSame =
                    TaEmp.SupervisedYear == ta.SupervisedYear;

                bool isUserDataSame =
                    user.UserId == ta.UserId &&
                    user.FirstName == ta.FirstName &&
                    user.LastName == ta.LastName &&
                    user.Email == ta.Email &&
                    user.Address == ta.Address &&
                    user.Password == ta.Password &&
                    user.DateOfBarth == ta.DateOfBarth &&
                    user.Gender == ta.Gender &&
                    user.PhoneNumber == ta.PhoneNumber &&
                    user.RoleId == ta.RoleId &&
                    user.AccountStatus == ta.AccountStatus &&
                    user.IdNumber == ta.IdNumber &&
                    user.FullName == ta.FirstName + " " + ta.LastName;

                if (isTaDataSame && isUserDataSame)
                {
                    return new GeneralMsgDto(
                            IErrorMsgs.You_Have_Oot_Made_Any_Modification_Please_Make_Any_Modifications_For_The_Modification_Process_To_Be_Successful,
                            "Failed",
                            "No changes were detected, update not required."
                        );
                }
                else
                {
                    TaEmp.SupervisedYear = ta.SupervisedYear;

                    user.UserId = ta.UserId;
                    user.FirstName = ta.FirstName;
                    user.LastName = ta.LastName;
                    user.Email = ta.Email;
                    user.Address = ta.Address;
                    user.Password = ta.Password;
                    user.DateOfBarth = ta.DateOfBarth;
                    user.Gender = ta.Gender;
                    user.PhoneNumber = ta.PhoneNumber;
                    user.RoleId = ta.RoleId;
                    user.AccountStatus = ta.AccountStatus;
                    user.IdNumber = ta.IdNumber;
                    user.FullName = ta.FirstName + " " + ta.LastName;

                    _appDbContext.Update(TaEmp);
                    _appDbContext.Update(user);
                    await _appDbContext.SaveChangesAsync();

                    return new GeneralMsgDto(
                        SuccessfullyMsgs.UPDATE_SUCCESSFUL,
                        "Update Successfully",
                        "You have updated student data successfully."
                    );
                }
            }
        }
    }
}
