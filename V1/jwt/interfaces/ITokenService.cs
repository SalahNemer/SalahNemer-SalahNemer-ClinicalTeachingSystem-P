using testDtoAndmapper.Entity;
using V1.DTO.LoginDTO;

namespace rafeeqeq.auth.jwt.interfaces;

public interface ITokenService
{
    string CreateToken(loginDto accountTbl);
    string CreateToken(Users result);
}