using DevetionStudetns.DTO.UserDTO;
using testDtoAndmapper.Entity;
using V1.DTO.UserDTO;

namespace DevetionStudetns.Mappers.UserMapper
{
    public static class GetUserMapper
    {
        public static GEtUserDto ShowUserMappers(this Users users)
        {
            return new GEtUserDto
            {
                UserId = users.UserId,
                IdNumber = users.IdNumber,
                FirstName = users.FirstName,
                LastName = users.LastName,
                FullName = users.FullName,
                DateOfBarth = users.DateOfBarth,
                Gender = users.Gender,
                roleId = (int)users.RoleId,
                PhoneNumber = users.PhoneNumber,
                Email = users.Email,
                Address = users.Address,
            };
        }

        public static GetAllUsersWithPaswwardDto ShowAllUserMappers(this Users users)
        {
            return new GetAllUsersWithPaswwardDto
            {
                UserId = users.UserId,
                IdNumber = users.IdNumber,
                FirstName = users.FirstName,
                LastName = users.LastName,
                FullName = users.FullName,
                DateOfBarth = users.DateOfBarth,
                Gender = users.Gender,
                roleId = (int)users.RoleId,
                PhoneNumber = users.PhoneNumber,
                Email = users.Email,
                Address = users.Address,
                Password = users.Password
            };
        }
    }
}
