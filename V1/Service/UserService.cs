using DevetionStudetns.DTO.UserDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using V1.DTO.UserDTO;

namespace DevetionStudetns.Service
{
    public class UserService
    {
        private readonly IUsers _context;
        public UserService(IUsers con)
        {
            _context = con;
        }
        public GeneralMsgDto AddUserService(AddUserDto userDto)
        {
            return _context.AddUser(userDto);
        }
        public GeneralMsgDto DeleteUserService(string userId)
        {
            return _context.DeleteUser(userId);
        }
        public GEtUserDto GetUserByIdService(string userName)
        {
            return _context.GetUserById(userName);
        }
        //public GeneralMsgDto UpdateUserService(AddUserDto NewData, string userId)
        //{
        //    return (_context.UpdateUser(NewData, userId));
        //}
        public GeneralMsgDto UpdateUserService(UpdateUserDto NewData, string userId)
        {
            return (_context.UpdateUser(NewData, userId));
        }
        public List<GetAllUsersWithPaswwardDto> GetAllUserUsersService()
        {
            return _context.GetAllUser();
        }
        public List<GEtUserDto> GetTheClinicalDepartmentDirectorAndDepartmentHeads()
        {
            return _context.GetTheClinicalDepartmentDirectorAndDepartmentHeads();
        }
    }
}
