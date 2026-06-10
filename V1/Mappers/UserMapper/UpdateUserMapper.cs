using FinalProject.Interface.IRepositry;
using testDtoAndmapper.Entity;
using V1.DTO.UserDTO;

namespace V1.Mappers.UserMapper
{
    public static class UpdateUserMapper
    {
        public static Users user(this UpdateUserDto users)
        {
            return new Users
            {
                FirstName = users.FirstName,

                LastName = users.LastName,

                Email = users.Email,

                Gender = users.Gender,

                Address = users.Address,

                PhoneNumber = users.PhoneNumber,

                DateOfBarth = users.DateOfBarth,

                RoleId = users.RoleId,

                AccountStatus = users.AccountStatus,

                Password = users.Password
            };
        }
    }
}
