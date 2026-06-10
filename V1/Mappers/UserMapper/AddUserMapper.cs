using DevetionStudetns.DTO.UserDTO;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.Mappers.UserMapper
{
    public static class AddUserMapper
    {
        public static Users AddUser (this AddUserDto usersDto)
        {
            return new Users
            {
                UserId = usersDto.UserId,
                IdNumber = usersDto.IdNumber,
                FirstName = usersDto.FirstName,
                LastName = usersDto.LastName,
                FullName = usersDto.FirstName + " " + usersDto.LastName,
                DateOfBarth = usersDto.DateOfBarth,
                Gender = usersDto.Gender,
                PhoneNumber = usersDto.PhoneNumber,
                Email = usersDto.Email,
                Address = usersDto.Address,
                Password = usersDto.Password,
                AccountStatus = usersDto.AccountStatus,
                RoleId = usersDto.RoleId,
            };
        }
    }
}
