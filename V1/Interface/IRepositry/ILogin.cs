using testDtoAndmapper.Entity;

namespace V1.Interface.IRepositry
{
    public interface ILogin
    {
        string GenerateJwtToken(Users user);
    }
}
