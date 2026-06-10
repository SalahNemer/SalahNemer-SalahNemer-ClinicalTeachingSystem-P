using DataBase.Entity;
using testDtoAndmapper.Entity;
using V1.DTO.StudentsDTO;
using V1.DTO.TADTO;

namespace V1.Mappers.TAMapper
{
    public static class UpdateTaAndUserMapper
    {
        public static TA UpdateTaMapper(this UpdateTaAndUserDto ta)
        {
            return new TA
            {
                SupervisedYear = ta.SupervisedYear
            };
        }
        public static Users UpdateUserMapper(this UpdateTaAndUserDto users)
        {
            return new Users
            {
                UserId = users.UserId,

                IdNumber = users.IdNumber,

                FirstName = users.FirstName,

                LastName = users.LastName,

                FullName = users.FullName,

                Email = users.Email,

                Gender = users.Gender,

                Address = users.Address,

                PhoneNumber = users.PhoneNumber,

                DateOfBarth = users.DateOfBarth,

                RoleId = users.RoleId,

                AccountStatus = users.AccountStatus,

                Password = users.Password,
            };
        }
    }
}
