using DataBase.DBcon;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.UserDTO;
using DevetionStudetns.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using testDtoAndmapper.Entity;
using V1.DTO.UserDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _context;
        private readonly DBC context;

        public UserController(UserService context1, DBC con)
        {
            _context = context1;
            context = con;
        }

        [HttpGet("GetUserByUserName/{UserId}")]
        public IActionResult GetUserByUserName(string UserId)
        {
            try
            {
                var getUserById = _context.GetUserByIdService(UserId);
                if (getUserById == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                            IErrorMsgs.USER_NOT_FOUND,
                                                            "Failed ",
                                                            "There is not any User have this id : " + UserId
                                                            );
                    return BadRequest(ErrorMsg);
                }
                return Ok(getUserById);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] AddUserDto user)
        {
            try
            {
                var result = _context.AddUserService(user);
                if ( result.ErrorMsg == "تم إنشاء الحساب بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPut("UpdateUser/{userId}")]
        public IActionResult UpdateUser([FromBody] UpdateUserDto user, string userId)
        {
            try
            {
                var result = _context.UpdateUserService(user, userId);
                if (result.ErrorMsg == "تم تحديث بيانات المستخدم بنجاح")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        //public IActionResult UpdateUser([FromBody] AddUserDto user, string userId)
        //{
        //    try
        //    {
        //       var result =_context.UpdateUserService(user, userId);
        //        if (result.ErrorMsg == "تم تحديث بيانات المستخدم بنجاح")
        //            return Ok(result);
        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
        //    }
        //}

        [HttpDelete("DeleteUserByUserName/{userId}")]
        public IActionResult DeleteUserByUserName(string userId)
        {
            try
            {
                return Ok(_context.DeleteUserService(userId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var getAllUser = context.Users.ToList().Count;
                var getUsersResult = _context.GetAllUserUsersService();
                if (getAllUser == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                 IErrorMsgs.USER_NOT_FOUND,
                                                                 "Failed ",
                                                                 "There is not any User in the user table "
                                                                 );
                    return BadRequest(ErrorMsg);
                }
                return Ok(getUsersResult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetTheClinicalDepartmentDirectorAndDepartmentHeads")]
        public IActionResult GetTheClinicalDepartmentDirectorAndDepartmentHeads()
        {
            try
            {
                var getAllUser = context.Users.Where(p => p.RoleId == 3 || p.RoleId == 4).ToList().Count;
                var getUsersResult = _context.GetTheClinicalDepartmentDirectorAndDepartmentHeads();
                if (getAllUser == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                 IErrorMsgs.USER_NOT_FOUND,
                                                                 "Failed ",
                                                                 "There is not any User in the user table "
                                                                 );
                    return BadRequest(ErrorMsg);
                }
                return Ok(getUsersResult);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}
