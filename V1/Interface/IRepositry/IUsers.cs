using DevetionStudetns.DTO.UserDTO;
using loginpage.DBcon;
using V1.DTO.UserDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IUsers
    {
        public GeneralMsgDto AddUser(AddUserDto userDto);
        public GeneralMsgDto DeleteUser(string userId);
        public GEtUserDto GetUserById(string userName);
        public GeneralMsgDto UpdateUser(UpdateUserDto NewData, string userId);
        public List<GetAllUsersWithPaswwardDto> GetAllUser();
        public List<GEtUserDto> GetTheClinicalDepartmentDirectorAndDepartmentHeads();
    }
}
