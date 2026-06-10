using DataBase.DBcon;
using DevetionStudetns.DTO.UserDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.UserMapper;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Identity.Client;
using testDtoAndmapper.Entity;
using V1.DTO.UserDTO;

namespace DevetionStudetns.Repositry.UsersReosetry
{
    public class UserRepo : IUsers
    {
        private readonly DBC _context;
        public UserRepo(DBC context)
        {
            _context = context;
        }

        public GeneralMsgDto AddUser(AddUserDto userDto)
        {
            var getUserIdDUPLICATE = _context.Users.Where(p => p.UserId == userDto.UserId).ToList().Count;

            if (getUserIdDUPLICATE > 0)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DUPLICATE_USER_ID,
                                        "Failed ",
                                        "DUPLICATE USER ID "
                                        );
                return ErrorMsg;
            }
            var getIdNumberDUPLICATE = _context.Users.Where(p => p.IdNumber == userDto.IdNumber).ToList().Count;
            var getEmailDUPLICATE = _context.Users.Where(p => p.Email == userDto.Email).ToList().Count;
            var getPhoneNumberDUPLICATE = _context.Users.Where(p => p.PhoneNumber == userDto.PhoneNumber).ToList().Count;

            if (userDto.RoleId <= 15 && userDto.RoleId >= 1)
            {
                if (getIdNumberDUPLICATE == 0)
                {
                    if (userDto.Gender.ToUpper() == "MALE" || userDto.Gender.ToUpper() == "FEMALE" || userDto.Gender.ToUpper() == "ذكر" || userDto.Gender.ToUpper() == "أنثى" || userDto.Gender.ToUpper() == "انثى")
                    {
                        if (getEmailDUPLICATE == 0)
                        {
                            if (getPhoneNumberDUPLICATE == 0)
                            {


                                _context.Users.Add(userDto.AddUser());
                                _context.SaveChanges();

                                GeneralMsgDto SuccessMsg = new GeneralMsgDto(
                                   SuccessfullyMsgs.ACCOUNT_CREATED_SUCCESSFULLY,
                                   "Successfully ",
                                   "You are Add new Account  "
                                   );
                                return SuccessMsg;

                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                       IErrorMsgs.DUPLICATE_PHONE_NUMBER,
                                       "Failed ",
                                       "Error Enter the corect phone number  "
                                       );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                       IErrorMsgs.DUPLICATE_Email,
                                       "Failed ",
                                       "Error Enter the corect  email "
                                       );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.GENDER_NOT_FOUND,
                                        "Failed ",
                                        "enter the data between male female ذكر أنثى "
                                        );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DUPLICATE_ID_NUMBER,
                                        "Failed ",
                                        "Error Enter the corect id number "
                                        );
                    return ErrorMsg;

                }
            }
            else
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.ROLE_ID_NOT_FOUND,
                                        "Failed ",
                                        "Error Enter the corect role id  "
                                        );
                return ErrorMsg;
            }
        }
        public GeneralMsgDto DeleteUser(string userId)
        {
            var getUserToDelete = _context.Users.FirstOrDefault(p => p.UserId == userId);
            if (getUserToDelete == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.USER_NOT_FOUND,
                                        "Failed ",
                                        "Error there is not any user have same this user id :" + userId
                                        );
                return ErrorMsg;
            }
            else
            {
                try
                {
                    _context.Users.Remove(getUserToDelete);
                    _context.SaveChanges();
                    GeneralMsgDto SUCCESSFULMsg = new GeneralMsgDto(
                                        SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                        "Successfully ",
                                        "You are delete this user :" + userId
                                        );
                    return SUCCESSFULMsg;
                }
                catch (Exception ex)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DELETE_ERROR,
                                        "Failed ",
                                        "Error when you delete user  , the user was not deleted"
                                        );
                    return ErrorMsg;
                }
            }
        }
        public GEtUserDto GetUserById(string userName)
        {
            var GetUserBy_Id = _context.Users.FirstOrDefault(p => p.UserId == userName);
            if (GetUserBy_Id != null)
            {
                GEtUserDto userDto = GetUserBy_Id.ShowUserMappers();
                return userDto;
            }
            return null;

        }


        public GeneralMsgDto UpdateUser(UpdateUserDto NewData, string userId)
        {

            var getUser = _context.Users.FirstOrDefault(p => p.UserId == userId);
            if (getUser == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.USER_NOT_FOUND,
                                        "Failed ",
                                        "Error there is not any user have same this user id :" + userId
                                        );
                return ErrorMsg;
            }
            else
            {
                //var getIdNumberDUPLICATE = _context.Users.Where(p => p.IdNumber == NewData.IdNumber && p.UserId != userId).ToList().Count;
                var getEmailDUPLICATE = _context.Users.Where(p => p.Email == NewData.Email && p.UserId != userId).ToList().Count;
                var getPhoneNumberDUPLICATE = _context.Users.Where(p => p.PhoneNumber == NewData.PhoneNumber && p.UserId != userId).ToList().Count;

                if (NewData.RoleId <= 15 && NewData.RoleId >= 1)
                {
                    //if (getIdNumberDUPLICATE == 0)
                    //{
                    if (NewData.Gender.ToUpper() == "MALE" || NewData.Gender.ToUpper() == "FEMALE" || NewData.Gender.ToUpper() == "ذكر" || NewData.Gender.ToUpper() == "أنثى" || NewData.Gender.ToUpper() == "انثى")
                    {
                        if (getEmailDUPLICATE == 0)
                        {
                            if (getPhoneNumberDUPLICATE == 0)
                            {

                                getUser.Email = NewData.Email;
                                getUser.Address = NewData.Address;
                                getUser.Password = NewData.Password;
                                getUser.DateOfBarth = NewData.DateOfBarth;
                                getUser.Gender = NewData.Gender;
                                getUser.PhoneNumber = NewData.PhoneNumber;
                                getUser.RoleId = NewData.RoleId;
                                getUser.AccountStatus = NewData.AccountStatus;
                                //getUser.IdNumber = NewData.IdNumber;
                                getUser.FullName = NewData.FirstName + " " + NewData.LastName;
                                getUser.FirstName = NewData.FirstName;
                                getUser.LastName = NewData.LastName;

                                _context.SaveChanges();

                                GeneralMsgDto SuccessMsg = new GeneralMsgDto(
                                   SuccessfullyMsgs.SETTINGS_UPDATED,
                                   "Successfully ",
                                   "You are Add new Account  "
                                   );
                                return SuccessMsg;
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                       IErrorMsgs.DUPLICATE_PHONE_NUMBER,
                                       "Failed ",
                                       "Error Enter the corect phone number  "
                                       );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                       IErrorMsgs.DUPLICATE_Email,
                                       "Failed ",
                                       "Error Enter the corect  email "
                                       );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.GENDER_NOT_FOUND,
                                        "Failed ",
                                        "enter the data between male female ذكر أنثى "
                                        );
                        return ErrorMsg;
                    }
                    //}
                    //else
                    //{
                    //    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                    //                        IErrorMsgs.DUPLICATE_ID_NUMBER,
                    //                        "Failed ",
                    //                        "Error Enter the corect id number "
                    //                        );
                    //    return ErrorMsg;

                    //}
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.ROLE_ID_NOT_FOUND,
                                            "Failed ",
                                            "Error Enter the corect role id  "
                                            );
                    return ErrorMsg;
                }
            }
        }


        //public GeneralMsgDto UpdateUser(AddUserDto NewData, string userId)
        //{

        //    var getUser = _context.Users.FirstOrDefault(p => p.UserId == userId);
        //    if (getUser == null)
        //    {
        //        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
        //                                IErrorMsgs.USER_NOT_FOUND,
        //                                "Failed ",
        //                                "Error there is not any user have same this user id :" + userId
        //                                );
        //        return ErrorMsg;
        //    }
        //    else
        //    {
        //        var getIdNumberDUPLICATE = _context.Users.Where(p => p.IdNumber == NewData.IdNumber && p.UserId != userId).ToList().Count;
        //        var getEmailDUPLICATE = _context.Users.Where(p => p.Email == NewData.Email && p.UserId != userId).ToList().Count;
        //        var getPhoneNumberDUPLICATE = _context.Users.Where(p => p.PhoneNumber == NewData.PhoneNumber && p.UserId != userId).ToList().Count;

        //        if (NewData.RoleId <= 15 && NewData.RoleId >= 1)
        //        {
        //            if (getIdNumberDUPLICATE == 0)
        //            {
        //                if (NewData.Gender.ToUpper() == "MALE" || NewData.Gender.ToUpper() == "FEMALE" || NewData.Gender.ToUpper() == "ذكر" || NewData.Gender.ToUpper() == "أنثى" || NewData.Gender.ToUpper() == "انثى")
        //                {
        //                    if (getEmailDUPLICATE == 0)
        //                    {
        //                        if (getPhoneNumberDUPLICATE == 0)
        //                        {

        //                            getUser.Email = NewData.Email;
        //                            getUser.Address = NewData.Address;
        //                            getUser.Password = NewData.Password;
        //                            getUser.DateOfBarth = NewData.DateOfBarth;
        //                            getUser.Gender = NewData.Gender;
        //                            getUser.PhoneNumber = NewData.PhoneNumber;
        //                            getUser.RoleId = NewData.RoleId;
        //                            getUser.AccountStatus = NewData.AccountStatus;
        //                            getUser.IdNumber = NewData.IdNumber;
        //                            getUser.FullName = NewData.FirstName + " " + NewData.LastName;

        //                            _context.SaveChanges();

        //                            GeneralMsgDto SuccessMsg = new GeneralMsgDto(
        //                               SuccessfullyMsgs.SETTINGS_UPDATED,
        //                               "Successfully ",
        //                               "You are Add new Account  "
        //                               );
        //                            return SuccessMsg;
        //                        }
        //                        else
        //                        {
        //                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
        //                                   IErrorMsgs.DUPLICATE_PHONE_NUMBER,
        //                                   "Failed ",
        //                                   "Error Enter the corect phone number  "
        //                                   );
        //                            return ErrorMsg;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
        //                                   IErrorMsgs.DUPLICATE_Email,
        //                                   "Failed ",
        //                                   "Error Enter the corect  email "
        //                                   );
        //                        return ErrorMsg;
        //                    }
        //                }
        //                else
        //                {
        //                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
        //                                    IErrorMsgs.GENDER_NOT_FOUND,
        //                                    "Failed ",
        //                                    "enter the data between male female ذكر أنثى "
        //                                    );
        //                    return ErrorMsg;
        //                }
        //            }
        //            else
        //            {
        //                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
        //                                    IErrorMsgs.DUPLICATE_ID_NUMBER,
        //                                    "Failed ",
        //                                    "Error Enter the corect id number "
        //                                    );
        //                return ErrorMsg;

        //            }
        //        }
        //        else
        //        {
        //            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
        //                                    IErrorMsgs.ROLE_ID_NOT_FOUND,
        //                                    "Failed ",
        //                                    "Error Enter the corect role id  "
        //                                    );
        //            return ErrorMsg;
        //        }
        //    }
        //}
        public List<GetAllUsersWithPaswwardDto> GetAllUser()
        {
            List<Users> users = _context.Users.ToList();
            List<GetAllUsersWithPaswwardDto> showUserDtos = new List<GetAllUsersWithPaswwardDto>();
            if (users == null)
                return null;
            foreach (Users user in users)
            {
                showUserDtos.Add(user.ShowAllUserMappers());
            }
            return showUserDtos;
        }
        public List<GEtUserDto> GetTheClinicalDepartmentDirectorAndDepartmentHeads()
        {
            List<Users> users = _context.Users.Where(p => p.RoleId == 3 ).ToList();
            List<GEtUserDto> showUserDtos = new List<GEtUserDto>();
            if (users == null)
                return null;
            foreach (Users user in users)
            {
                showUserDtos.Add(user.ShowUserMappers());
            }
            return showUserDtos;
        }
    }
}
