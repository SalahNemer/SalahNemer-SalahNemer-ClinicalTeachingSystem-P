using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using testDtoAndmapper.Entity;
using V1.Interface.IRepositry;
using Microsoft.Extensions.Configuration;
using DevetionStudetns.Repositry.UsersReosetry;
using Microsoft.AspNetCore.Identity;
using DataBase.DBcon;
using V1.jwt;
using rafeeqeq.auth.jwt.interfaces;
using V1.DTO.LoginDTO;

namespace V1.Service
{
    public class loginserves 
    {
        private readonly IConfiguration _config;
        private readonly DBC _context;
        private readonly ITokenService _tokenService;


        public loginserves(IConfiguration config, DBC con , ITokenService tokenService)
        {
            _config = config;
            _tokenService = tokenService;
            _context = con;
        }

        public LoginResponse login (loginDto login)
        {
            var adminUser = new loginDto
            {
                UserId = "Admin",
                Password = "Admin",
            };
            var result = _context.Users.FirstOrDefault(p=>p.UserId == login.UserId);
            if (adminUser.UserId == login.Password && login.UserId == adminUser.Password)
            {
                int convartRole = 1;
                var AccountStatus = 1;
                string Token = _tokenService.CreateToken(adminUser);
                return new LoginResponse("Successful Login", Token, convartRole, AccountStatus);
            }
            if (result == null)
            {
                return new LoginResponse("User not found ", "",0,0);
            }

            if (result.Password == login.Password)
            {
                var  role  = result.RoleId;
                int convartRole = Convert.ToInt16(role);
                var AccountStatus = Convert.ToInt16(result.AccountStatus);
                string Token = _tokenService.CreateToken(result);
                return new LoginResponse("Successful Login", Token, convartRole, AccountStatus);
            }

            return new LoginResponse("Invalid Your Username or Password  ", "", 0, 0);
        }
        
    }
}

